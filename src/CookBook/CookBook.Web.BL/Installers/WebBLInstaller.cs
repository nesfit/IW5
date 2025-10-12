using CookBook.Common.BL.Facades;
using CookBook.Web.BL.Api;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Web.BL.Installers;

public class WebBLInstaller
{
    public void Install(IServiceCollection serviceCollection, string apiBaseUrl)
    {
        AddApiClient<IIngredientApiClient, IngredientApiClient>(serviceCollection, apiBaseUrl);
        AddApiClient<IRecipeApiClient, RecipeApiClient>(serviceCollection, apiBaseUrl);

        serviceCollection.Scan(selector =>
            selector.FromAssemblyOf<WebBLInstaller>()
                .AddClasses(classes => classes.AssignableTo<IAppFacade>())
                .AsSelfWithInterfaces()
                .WithTransientLifetime());
    }

    private void AddApiClient<TInterface, TImplementation>(IServiceCollection serviceCollection, string apiBaseUrl)
        where TInterface : class, IApiClient
        where TImplementation : class, TInterface
    {
        serviceCollection.AddHttpClient<TInterface, TImplementation>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
        });
    }
}
