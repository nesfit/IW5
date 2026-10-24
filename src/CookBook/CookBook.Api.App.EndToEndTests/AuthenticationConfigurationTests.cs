using CookBook.Common.Options;
using Microsoft.AspNetCore.Authentication;
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
}
