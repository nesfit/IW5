using CookBook.Common.Options;
using CookBook.Web.BL.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Authentication.WebAssembly.Msal.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace CookBook.Web.BL.UnitTests;

public class WebAuthenticationTests
{
    [Theory]
    [InlineData(AuthenticationProviderType.Entra,
        "_content/Microsoft.Authentication.WebAssembly.Msal/AuthenticationService.js")]
    [InlineData(AuthenticationProviderType.CookBookIdentityProvider,
        "_content/Microsoft.AspNetCore.Components.WebAssembly.Authentication/AuthenticationService.js")]
    public void SelectsMatchingJavaScriptAdapter(AuthenticationProviderType providerType, string script)
    {
        Assert.Equal(script, CreateProvider(providerType).GetAuthenticationScriptPath());
    }

    [Fact]
    public void UnknownProviderFailsExplicitly()
    {
        var provider = CreateProvider((AuthenticationProviderType)999);

        Assert.Throws<NotSupportedException>(() => provider.GetAuthenticationScriptPath());
        Assert.Throws<NotSupportedException>(() => new ServiceCollection().AddCookBookAuthentication(provider));
    }

    [Fact]
    public void CookBookKeepsOidcConfiguration()
    {
        var services = CreateServices();
        var provider = CreateProvider(AuthenticationProviderType.CookBookIdentityProvider);
        services.AddCookBookAuthentication(provider);
        using var container = services.BuildServiceProvider();
        var options = container.GetRequiredService<IOptions<RemoteAuthenticationOptions<OidcProviderOptions>>>().Value;

        Assert.Equal(provider.Authority, options.ProviderOptions.Authority);
        Assert.Equal(provider.ClientId, options.ProviderOptions.ClientId);
        Assert.Equal(provider.RedirectUri, options.ProviderOptions.RedirectUri);
        Assert.Equal(provider.PostLogoutRedirectUri, options.ProviderOptions.PostLogoutRedirectUri);
        Assert.Equal("code", options.ProviderOptions.ResponseType);
        Assert.Equal(provider.DefaultScopes, options.ProviderOptions.DefaultScopes);
    }

    [Fact]
    public void EntraUsesMsalWithApiScopesInsteadOfGraphUserInfo()
    {
        var services = CreateServices();
        var provider = CreateProvider(AuthenticationProviderType.Entra);
        provider.DefaultScopes.Add("email");
        provider.DefaultScopes.Add("offline_access");
        services.AddCookBookAuthentication(provider);
        using var container = services.BuildServiceProvider();
        var options = container.GetRequiredService<IOptions<RemoteAuthenticationOptions<MsalProviderOptions>>>().Value;

        Assert.Equal(provider.Authority, options.ProviderOptions.Authentication.Authority);
        Assert.Equal(provider.ClientId, options.ProviderOptions.Authentication.ClientId);
        Assert.Equal(provider.RedirectUri, options.ProviderOptions.Authentication.RedirectUri);
        Assert.Equal(provider.PostLogoutRedirectUri, options.ProviderOptions.Authentication.PostLogoutRedirectUri);
        Assert.True(options.ProviderOptions.Authentication.ValidateAuthority);
        Assert.Equal("redirect", options.ProviderOptions.LoginMode);
        Assert.Equal(["api://cookbook/access_as_user"], options.ProviderOptions.DefaultAccessTokenScopes);
        Assert.Equal("roles", options.UserOptions.RoleClaim);
        Assert.DoesNotContain(services, service =>
            service.ServiceType == typeof(IConfigureOptions<RemoteAuthenticationOptions<OidcProviderOptions>>));
    }

    private static ServiceCollection CreateServices()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddSingleton<NavigationManager>(new TestNavigationManager());
        return services;
    }

    private static WebAuthenticationProviderOptions CreateProvider(AuthenticationProviderType type) => new()
    {
        ProviderType = type,
        IsEnabled = true,
        Authority = "https://login.microsoftonline.com/test-tenant/v2.0",
        ClientId = "test-client",
        RedirectUri = "https://localhost:44355/authentication/login-callback",
        PostLogoutRedirectUri = "https://localhost:44355/",
        DefaultScopes = ["openid", "profile", "api://cookbook/access_as_user"]
    };

    private sealed class TestNavigationManager : NavigationManager
    {
        public TestNavigationManager() => Initialize("https://localhost:44355/", "https://localhost:44355/");
    }
}
