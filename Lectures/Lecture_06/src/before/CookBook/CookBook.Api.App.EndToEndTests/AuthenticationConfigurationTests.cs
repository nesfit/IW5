using System.Security.Claims;
using CookBook.Api.App.Authorization;
using CookBook.Common.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace CookBook.Api.App.EndToEndTests;

public class AuthenticationConfigurationTests
{
    [Fact]
    public async Task AllConfiguredTrustedIssuersAreRegistered()
    {
        await using var application = new CookBookApiApplicationFactory();
        using var scope = application.Services.CreateScope();
        var configuredIssuers = scope.ServiceProvider.GetRequiredService<IConfiguration>()
            .GetSection(nameof(TrustedIssuersOptions)).Get<TrustedIssuersOptions>();
        var schemes = await scope.ServiceProvider
            .GetRequiredService<IAuthenticationSchemeProvider>()
            .GetAllSchemesAsync();

        Assert.NotNull(configuredIssuers);
        Assert.NotEmpty(configuredIssuers.Issuers);
        foreach (var issuer in configuredIssuers.Issuers)
        {
            Assert.Contains(schemes, scheme => scheme.Name == issuer.Scheme);
        }
    }

    [Fact]
    public async Task AnonymousReadEndpointRemainsAvailable_WhenIdentityIsDisabled()
    {
        await using var application = new CookBookApiApplicationFactory();
        using var client = application.CreateClient();

        var response = await client.GetAsync("/api/recipe");

        response.EnsureSuccessStatusCode();
    }

    [Theory]
    [InlineData("api.read access_as_user", null, true)]
    [InlineData(null, "user", true)]
    [InlineData("Recipes.Read", "user", false)]
    [InlineData("access_as_user", "user", true)]
    [InlineData(null, "admin", true)]
    [InlineData("Recipes.Read", null, false)]
    [InlineData(null, null, false)]
    public void EntraTokensNeedAUserScopeOrApplicationRole(string? scopes, string? role, bool expected)
    {
        var claims = new List<Claim>();
        if (scopes is not null)
        {
            claims.Add(new Claim("scp", scopes));
        }
        if (role is not null)
        {
            claims.Add(new Claim("roles", role));
        }

        var identity = new ClaimsIdentity(claims);

        Assert.Equal(expected, EntraTokenAuthorization.HasApiAccess(identity, "roles"));
    }

    [Fact]
    public void EntraScopeClaimAndRoleMustStillContainRequiredScope()
    {
        var identity = new ClaimsIdentity(
        [
            new Claim("scope", "Recipes.Read"),
            new Claim("roles", "user")
        ]);

        Assert.False(EntraTokenAuthorization.HasApiAccess(identity, "roles"));
    }

    [Fact]
    public async Task EmptyTrustedIssuerListConfiguresAnonymousAuthentication()
    {
        var services = new ServiceCollection();

        TrustedIssuerAuthenticationConfiguration.Configure(
            services,
            new TrustedIssuersOptions(),
            new IdentityOptions { IsEnabled = false });

        using var serviceProvider = services.BuildServiceProvider();
        var schemes = await serviceProvider
            .GetRequiredService<IAuthenticationSchemeProvider>()
            .GetAllSchemesAsync();

        Assert.Empty(schemes);
        Assert.Null(serviceProvider.GetRequiredService<IOptions<AuthenticationOptions>>().Value.DefaultScheme);
    }
}
