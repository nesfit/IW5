using CookBook.Common.Options;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Web.BL.Extensions;

public static class WebAuthenticationExtensions
{
    public static string GetAuthenticationScriptPath(this WebAuthenticationProviderOptions provider)
        => provider.ProviderType switch
        {
            AuthenticationProviderType.Entra =>
                "_content/Microsoft.Authentication.WebAssembly.Msal/AuthenticationService.js",
            AuthenticationProviderType.CookBookIdentityProvider =>
                "_content/Microsoft.AspNetCore.Components.WebAssembly.Authentication/AuthenticationService.js",
            _ => throw new NotSupportedException($"Authentication provider '{provider.ProviderType}' is not supported.")
        };

    public static IServiceCollection AddCookBookAuthentication(
        this IServiceCollection services,
        WebAuthenticationProviderOptions provider)
    {
        if (provider.ProviderType == AuthenticationProviderType.Entra)
        {
            services.AddMsalAuthentication(options =>
            {
                options.ProviderOptions.Authentication.Authority = provider.Authority;
                options.ProviderOptions.Authentication.ClientId = provider.ClientId;
                options.ProviderOptions.Authentication.ValidateAuthority = true;
                options.ProviderOptions.LoginMode = "redirect";
                options.UserOptions.RoleClaim = "roles";

                if (!string.IsNullOrWhiteSpace(provider.RedirectUri))
                {
                    options.ProviderOptions.Authentication.RedirectUri = provider.RedirectUri;
                }

                if (!string.IsNullOrWhiteSpace(provider.PostLogoutRedirectUri))
                {
                    options.ProviderOptions.Authentication.PostLogoutRedirectUri = provider.PostLogoutRedirectUri;
                }

                // MSAL adds OIDC scopes itself; these scopes are only for the API access token.
                foreach (var scope in provider.DefaultScopes.Where(scope =>
                             scope is not ("openid" or "profile" or "email" or "offline_access")))
                {
                    options.ProviderOptions.DefaultAccessTokenScopes.Add(scope);
                }
            });
            return services;
        }

        if (provider.ProviderType != AuthenticationProviderType.CookBookIdentityProvider)
        {
            throw new NotSupportedException($"Authentication provider '{provider.ProviderType}' is not supported.");
        }

        services.AddOidcAuthentication(options =>
        {
            options.ProviderOptions.Authority = provider.Authority;
            options.ProviderOptions.ClientId = provider.ClientId;
            options.ProviderOptions.ResponseType = provider.ResponseType;
            if (!string.IsNullOrWhiteSpace(provider.RedirectUri))
            {
                options.ProviderOptions.RedirectUri = provider.RedirectUri;
            }

            if (!string.IsNullOrWhiteSpace(provider.PostLogoutRedirectUri))
            {
                options.ProviderOptions.PostLogoutRedirectUri = provider.PostLogoutRedirectUri;
            }

            options.ProviderOptions.DefaultScopes.Clear();
            foreach (var scope in provider.DefaultScopes)
            {
                options.ProviderOptions.DefaultScopes.Add(scope);
            }
        });
        return services;
    }
}
