using System.Security.Claims;
using Duende.IdentityServer.Test;
using Duende.IdentityModel;

namespace CookBook.IdentityProvider.App.Services;

public sealed class TestStoreExternalUserService(TestUserStore users) : IExternalUserService
{
    public Task<ExternalUser> FindOrProvisionAsync(
        string provider,
        string providerSubject,
        string? verifiedEmail,
        ClaimsPrincipal externalPrincipal,
        CancellationToken cancellationToken)
    {
        var user = users.FindByExternalProvider(provider, providerSubject);
        if (user is null)
        {
            var claims = externalPrincipal.Claims
                .Where(claim => claim.Type is not JwtClaimTypes.Subject and not ClaimTypes.NameIdentifier)
                .ToList();
            user = users.AutoProvisionUser(provider, providerSubject, claims);
        }

        var displayName = ExternalUserDisplayName.GetDisplayName(externalPrincipal);
        // Provider roles are authorization data owned by the provider, so they are replaced on every login.
        // They are stored as JwtClaimTypes.Role because the test profile service only emits requested claim types.
        var currentProviderRoles = externalPrincipal.Claims
            .Where(claim => IsRoleClaim(claim.Type))
            .Select(claim => claim.Value)
            .Distinct(StringComparer.Ordinal)
            .Select(role => new Claim(JwtClaimTypes.Role, role));
        user.Claims = user.Claims
            .Where(claim => claim.Type is not "username" and not JwtClaimTypes.Name && !IsRoleClaim(claim.Type))
            .Concat([new Claim("username", displayName), new Claim(JwtClaimTypes.Name, displayName)])
            .Concat(currentProviderRoles)
            .ToList();

        return Task.FromResult(new ExternalUser(
            user.SubjectId,
            displayName,
            user.Claims.ToList()));
    }

    private static bool IsRoleClaim(string claimType)
        => claimType is JwtClaimTypes.Role or "roles" or ClaimTypes.Role;
}
