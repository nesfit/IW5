using CookBook.Maui.BL.Api;
using CookBook.Maui.BL.Facades;
using CookBook.Maui.BL.Factories;
using CookBook.Maui.BL.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Maui.BL.Installers;

public class MauiBLInstaller
{
    public void Install(IServiceCollection serviceCollection, string apiBaseUrl)
    {
        serviceCollection.AddSingleton<IIngredientFacade, IngredientFacade>();
        serviceCollection.AddSingleton<ICommandFactory, CommandFactory>();

        serviceCollection.AddTransient<IRecipeApiClient, RecipeApiClient>(provider =>
        {
            var client = CreateApiHttpClient(provider, apiBaseUrl);
            return new RecipeApiClient(client, apiBaseUrl);
        });

        serviceCollection.AddTransient<IIngredientApiClient, IngredientApiClient>(provider =>
        {
            var client = CreateApiHttpClient(provider, apiBaseUrl);
            return new IngredientApiClient(client, apiBaseUrl);
        });

        serviceCollection.AddSingleton<IngredientListViewModel>();
        serviceCollection.AddSingleton<IngredientDetailViewModel>();
    }

    private HttpClient CreateApiHttpClient(IServiceProvider serviceProvider, string apiBaseUrl)
    {
        var client = new HttpClient() { BaseAddress = new Uri(apiBaseUrl) };
        client.BaseAddress = new Uri(apiBaseUrl);
        return client;
    }
}
