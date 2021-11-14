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
        serviceCollection.AddSingleton<ICommandFactory, CommandFactory>();

        serviceCollection.AddTransient<IRecipeApiClient, RecipeApiClient>(_ =>
        {
            var client = CreateApiHttpClient(apiBaseUrl);
            return new RecipeApiClient(client, apiBaseUrl);
        });

        serviceCollection.AddTransient<IIngredientApiClient, IngredientApiClient>(_ =>
        {
            var client = CreateApiHttpClient(apiBaseUrl);
            return new IngredientApiClient(client, apiBaseUrl);
        });

        serviceCollection.Scan(selector =>
            selector.FromAssemblyOf<MauiBLInstaller>()
                .AddClasses(classes => classes.AssignableTo(typeof(IListFacade<>)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());

        serviceCollection.Scan(selector =>
            selector.FromAssemblyOf<MauiBLInstaller>()
                .AddClasses(classes => classes.AssignableTo(typeof(IDetailFacade<>)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());

        serviceCollection.Scan(selector =>
            selector.FromAssemblyOf<MauiBLInstaller>()
                .AddClasses(classes => classes.AssignableTo<IViewModel>())
                .AsSelfWithInterfaces()
                .WithTransientLifetime());

        serviceCollection.AddTransient(typeof(ListViewModelBase<,>.Dependencies));
        serviceCollection.AddTransient(typeof(DetailViewModelBase<>.Dependencies));
        serviceCollection.AddTransient(typeof(EditViewModelBase<>.Dependencies));
    }

    private HttpClient CreateApiHttpClient(string apiBaseUrl)
    {
        var client = new HttpClient { BaseAddress = new Uri(apiBaseUrl) };
        client.BaseAddress = new Uri(apiBaseUrl);
        return client;
    }
}
