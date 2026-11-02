using System.Text.Json;
using CookBook.Common.BL.Facades;
using CookBook.Common.Options;
using CookBook.Web.App.Options;
using CookBook.Web.BL.Api;
using CookBook.Web.BL.MessageHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Web.BL.Installers;

public class WebBLInstaller
{
    public void Install(
        IServiceCollection serviceCollection,
        IdentityOptions identityOptions,
        WebAuthenticationProviderOptions? selectedProvider,
        ApiOptions apiOptions)
    {
        if (identityOptions.IsEnabled && selectedProvider is not null)
        {
            serviceCollection.AddScoped<CustomAuthorizationMessageHandler>();
        }
        serviceCollection.AddSingleton<JsonSerializerOptions>(new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        });

        AddApiClient<IIngredientApiClient, IngredientApiClient>(serviceCollection, identityOptions, selectedProvider, apiOptions);
        AddApiClient<IRecipeApiClient, RecipeApiClient>(serviceCollection, identityOptions, selectedProvider, apiOptions);

        serviceCollection.Scan(selector =>
            selector.FromAssemblyOf<WebBLInstaller>()
                .AddClasses(classes => classes.AssignableTo<IAppFacade>())
                .AsSelfWithInterfaces()
                .WithTransientLifetime());
    }

    private void AddApiClient<TInterface, TImplementation>(
        IServiceCollection serviceCollection,
        IdentityOptions identityOptions,
        WebAuthenticationProviderOptions? selectedProvider,
        ApiOptions apiOptions)
        where TInterface : class, IApiClient
        where TImplementation : class, TInterface
    {
        var httpClient = serviceCollection.AddHttpClient<TInterface, TImplementation>(client =>
        {
            client.BaseAddress = new Uri(apiOptions.BaseUrl);
        });

        if (identityOptions.IsEnabled && selectedProvider is not null)
        {
            httpClient.AddHttpMessageHandler(serviceProvider
                => serviceProvider.GetRequiredService<CustomAuthorizationMessageHandler>()
                .ConfigureHandler(
                    authorizedUrls: [apiOptions.BaseUrl],
                    scopes: selectedProvider.DefaultScopes));
        }
    }
}
