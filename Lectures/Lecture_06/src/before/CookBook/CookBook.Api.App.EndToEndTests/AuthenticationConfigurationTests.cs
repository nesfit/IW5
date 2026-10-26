using System.Security.Claims;
using CookBook.Api.App.Authorization;
using CookBook.Common.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
    public async Task EmptyTrustedIssuerListWorksWhenIdentityIsDisabled()
    {
        await using var application = new EmptyIssuerApiApplicationFactory();
        using var client = application.CreateClient();

        var response = await client.GetAsync("/api/recipe");

        response.EnsureSuccessStatusCode();
    }

    private sealed class EmptyIssuerApiApplicationFactory : CookBookApiApplicationFactory
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((_, configuration) =>
            {
                configuration.Sources.Clear();
                configuration.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["DALSelectionOptions:Type"] = "Memory",
                    ["IdentityOptions:IsEnabled"] = "false"
                });
            });
        }
    }
}
