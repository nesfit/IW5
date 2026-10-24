using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CookBook.Api.App.EndToEndTests;

public class AuthenticationConfigurationTests
{
    [Fact]
    public async Task AllConfiguredTrustedIssuersAreRegistered_WhenIdentityIsDisabled()
    {
        await using var application = new CookBookApiApplicationFactory();
        using var scope = application.Services.CreateScope();
        var schemes = await scope.ServiceProvider
            .GetRequiredService<IAuthenticationSchemeProvider>()
            .GetAllSchemesAsync();

        Assert.Contains(schemes, scheme => scheme.Name == "CookBookIdentityProvider");
        Assert.Contains(schemes, scheme => scheme.Name == "Entra");
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
