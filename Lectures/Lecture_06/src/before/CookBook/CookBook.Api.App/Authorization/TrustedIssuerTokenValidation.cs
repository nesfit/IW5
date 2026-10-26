using System.Security.Claims;
using CookBook.Common.Options;

namespace CookBook.Api.App.Authorization;

public static class TrustedIssuerTokenValidation
{
    private static readonly string[] ScopeClaimTypes = ["scp", "scope"];

    public static void ValidateOptions(TrustedIssuersOptions trustedIssuersOptions, IdentityOptions identityOptions)
    {
        if (identityOptions.IsEnabled && trustedIssuersOptions.Issuers.Count == 0)
        {
            throw new InvalidOperationException(
                "TrustedIssuersOptions:Issuers must contain at least one entry when IdentityOptions:IsEnabled is true.");
        }

        foreach (var issuer in trustedIssuersOptions.Issuers)
        {
            if (string.IsNullOrWhiteSpace(issuer.Scheme)
                || string.IsNullOrWhiteSpace(issuer.Authority)
                || issuer.ValidIssuers.Count == 0
                || issuer.ValidAudiences.Count == 0
                || string.IsNullOrWhiteSpace(issuer.NameClaimType)
                || string.IsNullOrWhiteSpace(issuer.RoleClaimType))
            {
                throw new InvalidOperationException(
                    $"TrustedIssuersOptions entry for provider '{issuer.ProviderType}' is missing Scheme, Authority, ValidIssuers, ValidAudiences, NameClaimType, or RoleClaimType.");
            }
        }

        var duplicateSchemes = trustedIssuersOptions.Issuers
            .GroupBy(issuer => issuer.Scheme, StringComparer.Ordinal)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key)
            .ToList();

        if (duplicateSchemes.Count > 0)
        {
            throw new InvalidOperationException(
                $"TrustedIssuersOptions contains duplicate Scheme values: {string.Join(", ", duplicateSchemes)}");
        }

        // The scheme is selected by the token's `iss`, so one issuer value must map to exactly one scheme.
        var duplicateIssuers = trustedIssuersOptions.Issuers
            .SelectMany(issuer => issuer.ValidIssuers.Distinct(StringComparer.Ordinal))
            .GroupBy(validIssuer => validIssuer, StringComparer.Ordinal)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key)
            .ToList();

        if (duplicateIssuers.Count > 0)
        {
            throw new InvalidOperationException(
                $"TrustedIssuersOptions contains ValidIssuers shared by more than one Scheme: {string.Join(", ", duplicateIssuers)}");
        }
    }

    /// <summary>
    /// Normalizes a validated token's claims to the API's internal claim types. Returns an error message
    /// when the token must be rejected, otherwise null.
    /// </summary>
    public static string? NormalizeClaims(ClaimsIdentity identity, TrustedIssuerOptions issuer)
    {
        var roleValues = identity.FindAll(issuer.RoleClaimType)
            .Select(claim => claim.Value)
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .Distinct(StringComparer.Ordinal)
            .ToList();

        if (issuer.ProviderType == AuthenticationProviderType.Entra
            && issuer.RequiredScopes.Count == 0
            && !EntraTokenAuthorization.HasApiAccess(identity, issuer.RoleClaimType))
        {
            return "The Entra token must contain the access_as_user scope or an application role.";
        }

        if (issuer.RequiredScopes.Count > 0 && roleValues.Count == 0)
        {
            var grantedScopes = ScopeClaimTypes
                .SelectMany(identity.FindAll)
                .SelectMany(claim => claim.Value.Split(' ', StringSplitOptions.RemoveEmptyEntries));

            if (!grantedScopes.Intersect(issuer.RequiredScopes, StringComparer.Ordinal).Any())
            {
                return $"Token for scheme '{issuer.Scheme}' has neither a required scope ({string.Join(", ", issuer.RequiredScopes)}) nor an application role.";
            }
        }

        var name = identity.FindFirst(issuer.NameClaimType)?.Value;

        // Internal claim types are owned by the API: never trust values supplied directly in the token.
        RemoveAll(identity, ClaimTypes.NameIdentifier);
        RemoveAll(identity, ResourceOwnerClaims.OwnerIdClaimType);
        RemoveAll(identity, ClaimTypes.Role);

        if (name is not null)
        {
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, name));
            identity.AddClaim(new Claim(
                ResourceOwnerClaims.OwnerIdClaimType,
                ResourceOwnerClaims.CreateOwnerId(issuer.Scheme, name)));
        }

        foreach (var role in roleValues)
        {
            identity.AddClaim(new Claim(ClaimTypes.Role, role));
        }

        return null;
    }

    private static void RemoveAll(ClaimsIdentity identity, string claimType)
    {
        foreach (var claim in identity.FindAll(claimType).ToList())
        {
            identity.RemoveClaim(claim);
        }
    }
}
