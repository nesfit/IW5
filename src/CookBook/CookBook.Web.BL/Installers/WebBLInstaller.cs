using CookBook.Common.BL.Facades;
using CookBook.Common.Options;
using CookBook.Web.App.Options;
using CookBook.Web.BL.Api;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Web.BL.Installers;

public class WebBLInstaller
{
    public void Install(
        IServiceCollection serviceCollection,
        IdentityOptions identityOptions,
        ApiOptions apiOptions)
    {
        AddApiHttpClients(serviceCollection, identityOptions, apiOptions);

        serviceCollection.Scan(selector =>
            selector.FromAssemblyOf<WebBLInstaller>()
                .AddClasses(classes => classes.AssignableTo<IAppFacade>())
                .AsSelfWithInterfaces()
                .WithTransientLifetime());
    }

    private static void AddApiHttpClients(
        IServiceCollection serviceCollection,
        IdentityOptions identityOptions,
        ApiOptions apiOptions)
    {
        serviceCollection.AddHttpClient(ApiHttpClientNames.Anonymous, client =>
        {
            client.BaseAddress = new Uri(apiOptions.BaseUrl);
        });

        var httpClient = serviceCollection.AddHttpClient(ApiHttpClientNames.Default, client =>
        {
            client.BaseAddress = new Uri(apiOptions.BaseUrl);
        });

        if (identityOptions.IsEnabled)
        {
            httpClient.AddHttpMessageHandler(serviceProvider
                => new AuthorizationMessageHandler(
                        serviceProvider.GetRequiredService<IAccessTokenProvider>(),
                        serviceProvider.GetRequiredService<NavigationManager>())
                    .ConfigureHandler(
                     authorizedUrls: [apiOptions.BaseUrl],
                     scopes: identityOptions.DefaultScopes));
        }
    }
}
