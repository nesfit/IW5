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
        ApiOptions apiOptions)
    {
        serviceCollection.AddScoped<CustomAuthorizationMessageHandler>();

        AddApiClient<IIngredientApiClient, IngredientApiClient>(serviceCollection, identityOptions, apiOptions);
        AddApiClient<IRecipeApiClient, RecipeApiClient>(serviceCollection, identityOptions, apiOptions);

        serviceCollection.Scan(selector =>
            selector.FromAssemblyOf<WebBLInstaller>()
                .AddClasses(classes => classes.AssignableTo<IAppFacade>())
                .AsSelfWithInterfaces()
                .WithTransientLifetime());
    }

    private void AddApiClient<TInterface, TImplementation>(
        IServiceCollection serviceCollection,
        IdentityOptions identityOptions,
        ApiOptions apiOptions)
        where TInterface : class, IApiClient
        where TImplementation : class, TInterface
    {
        var httpClient = serviceCollection.AddHttpClient<TInterface, TImplementation>(client =>
        {
            client.BaseAddress = new Uri(apiOptions.BaseUrl);
        });

        if (identityOptions.IsIdentityEnabled)
        {
            httpClient.AddHttpMessageHandler(serviceProvider
                => serviceProvider.GetRequiredService<CustomAuthorizationMessageHandler>()
                .ConfigureHandler(
                    authorizedUrls: [apiOptions.BaseUrl],
                    scopes: identityOptions.DefaultScopes));
        }
    }
}
