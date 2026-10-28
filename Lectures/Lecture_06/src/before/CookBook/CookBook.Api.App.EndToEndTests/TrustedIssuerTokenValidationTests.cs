using System.Security.Claims;
using CookBook.Api.App.Authorization;
using CookBook.Common.Options;
using Xunit;

namespace CookBook.Api.App.EndToEndTests;

public class TrustedIssuerTokenValidationTests
{
    [Fact]
    public void NormalizeClaims_Replaces_Spoofed_Internal_Claims()
    {
        var issuer = CreateIssuer("entra", nameClaimType: "oid", roleClaimType: "roles");
        issuer.ProviderType = AuthenticationProviderType.Entra;
        var identity = new ClaimsIdentity(
        [
            new Claim("oid", "real-object-id"),
            new Claim("scp", "access_as_user"),
            new Claim(ResourceOwnerClaims.OwnerIdClaimType, "local:victim"),
            new Claim(ClaimTypes.Role, "admin"),
            new Claim(ClaimTypes.NameIdentifier, "victim"),
        ], "test");

        var failure = TrustedIssuerTokenValidation.NormalizeClaims(identity, issuer);

        Assert.Null(failure);
        Assert.Equal("entra:real-object-id", Assert.Single(identity.FindAll(ResourceOwnerClaims.OwnerIdClaimType)).Value);
        Assert.Equal("real-object-id", Assert.Single(identity.FindAll(ClaimTypes.NameIdentifier)).Value);
        Assert.Empty(identity.FindAll(ClaimTypes.Role));
    }

    [Fact]
    public void NormalizeClaims_Maps_Issuer_Roles()
    {
        var issuer = CreateIssuer("entra", nameClaimType: "oid", roleClaimType: "roles");
        var identity = new ClaimsIdentity([new Claim("oid", "app"), new Claim("roles", "admin")], "test");

        var failure = TrustedIssuerTokenValidation.NormalizeClaims(identity, issuer);

        Assert.Null(failure);
        Assert.Equal("admin", Assert.Single(identity.FindAll(ClaimTypes.Role)).Value);
    }

    [Fact]
    public void NormalizeClaims_Fails_Without_Required_Scope_Or_Role()
    {
        var issuer = CreateIssuer("entra", nameClaimType: "oid", roleClaimType: "roles");
        var identity = new ClaimsIdentity([new Claim("oid", "user"), new Claim("scp", "User.Read other")], "test");

        Assert.NotNull(TrustedIssuerTokenValidation.NormalizeClaims(identity, issuer));
    }

    [Fact]
    public void NormalizeClaims_Accepts_Required_Scope_In_Space_Separated_List()
    {
        var issuer = CreateIssuer("entra", nameClaimType: "oid", roleClaimType: "roles");
        var identity = new ClaimsIdentity([new Claim("oid", "user"), new Claim("scp", "User.Read access_as_user")], "test");

        Assert.Null(TrustedIssuerTokenValidation.NormalizeClaims(identity, issuer));
    }

    [Fact]
    public void NormalizeClaims_Rejects_Entra_Token_Without_Required_Access()
    {
        var issuer = CreateIssuer("entra", nameClaimType: "oid", roleClaimType: "roles");
        issuer.ProviderType = AuthenticationProviderType.Entra;
        var identity = new ClaimsIdentity(
        [
            new Claim("oid", "user"),
            new Claim("scp", "User.Read")
        ], "test");

        Assert.NotNull(TrustedIssuerTokenValidation.NormalizeClaims(identity, issuer));
    }

    [Fact]
    public void NormalizeClaims_Rejects_Delegated_Token_With_Role_But_Wrong_Scope()
    {
        var issuer = CreateIssuer("entra", nameClaimType: "oid", roleClaimType: "roles");
        issuer.ProviderType = AuthenticationProviderType.Entra;
        var identity = new ClaimsIdentity(
        [
            new Claim("oid", "user"),
            new Claim("scp", "Recipes.Read"),
            new Claim("roles", "user")
        ], "test");

        Assert.NotNull(TrustedIssuerTokenValidation.NormalizeClaims(identity, issuer));
    }

    [Fact]
    public void NormalizeClaims_Accepts_App_Role_Token_Without_Scope_Claims()
    {
        var issuer = CreateIssuer("entra", nameClaimType: "oid", roleClaimType: "roles");
        issuer.ProviderType = AuthenticationProviderType.Entra;
        var identity = new ClaimsIdentity(
        [
            new Claim("oid", "application"),
            new Claim("roles", "CookBook.Write")
        ], "test");

        Assert.Null(TrustedIssuerTokenValidation.NormalizeClaims(identity, issuer));
        Assert.Equal("CookBook.Write", Assert.Single(identity.FindAll(ClaimTypes.Role)).Value);
    }

    [Fact]
    public void ValidateOptions_Rejects_ValidIssuer_Shared_By_Two_Schemes()
    {
        var options = new TrustedIssuersOptions
        {
            Issuers = [CreateIssuer("first"), CreateIssuer("second")]
        };

        var exception = Assert.Throws<InvalidOperationException>(
            () => TrustedIssuerTokenValidation.ValidateOptions(options, new IdentityOptions { IsEnabled = true }));
        Assert.Contains("https://issuer.example", exception.Message);
    }

    [Fact]
    public void ValidateOptions_Rejects_Enabled_Identity_Without_Issuers()
    {
        Assert.Throws<InvalidOperationException>(() => TrustedIssuerTokenValidation.ValidateOptions(
            new TrustedIssuersOptions(), new IdentityOptions { IsEnabled = true }));
    }

    [Fact]
    public void ValidateOptions_Rejects_Entra_Issuer_Without_Required_Scopes()
    {
        var issuer = CreateIssuer("entra");
        issuer.ProviderType = AuthenticationProviderType.Entra;
        issuer.RequiredScopes = [];

        var exception = Assert.Throws<InvalidOperationException>(
            () => TrustedIssuerTokenValidation.ValidateOptions(
                new TrustedIssuersOptions { Issuers = [issuer] },
                new IdentityOptions { IsEnabled = true }));

        Assert.Contains("RequiredScopes", exception.Message);
    }

    [Fact]
    public void ValidateOptions_Rejects_Issuer_Without_Explicit_ProviderType()
    {
        var issuer = CreateIssuer("issuer");
        issuer.ProviderType = null;

        Assert.Throws<InvalidOperationException>(
            () => TrustedIssuerTokenValidation.ValidateOptions(
                new TrustedIssuersOptions { Issuers = [issuer] },
                new IdentityOptions { IsEnabled = true }));
    }

    private static TrustedIssuerOptions CreateIssuer(string scheme, string nameClaimType = "sub", string roleClaimType = "role")
        => new()
        {
            Scheme = scheme,
            ProviderType = AuthenticationProviderType.CookBookIdentityProvider,
            Authority = "https://issuer.example",
            ValidIssuers = ["https://issuer.example"],
            ValidAudiences = ["api"],
            NameClaimType = nameClaimType,
            RoleClaimType = roleClaimType,
            RequiredScopes = ["access_as_user"],
        };
}
