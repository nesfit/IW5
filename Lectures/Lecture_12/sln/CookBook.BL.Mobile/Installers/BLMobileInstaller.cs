using CookBook.BL.Common.Facades;
using CookBook.BL.Mobile.Api;
using CookBook.BL.Mobile.Services;
using CookBook.BL.Mobile.ViewModels;
using CookBook.Common.Installers;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace CookBook.BL.Mobile.Installers
{
    public class BLMobileInstaller : IInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IIngredientClient, IngredientClient>(factory =>
                new IngredientClient(factory.GetService<HttpClient>()) { BaseUrl = "https://app-iw5-2020z-api.azurewebsites.net" });
            serviceCollection.AddTransient<IRecipeClient, RecipeClient>(factory =>
                new RecipeClient(factory.GetService<HttpClient>()) { BaseUrl = "https://app-iw5-2020z-api.azurewebsites.net" });

            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<BLMobileInstaller>()
                .AddClasses(classes => classes.AssignableTo<IAppFacade>())
                    .AsSelfWithInterfaces()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo<IViewModel>())
                    .AsSelf()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo<ISingletonService>())
                    .AsMatchingInterface()
                    .WithSingletonLifetime());
        }
    }
}