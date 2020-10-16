using CookBook.BL.Common.Facades;
using CookBook.BL.Web.MVC.Api;
using CookBook.Common.Installers;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using IngredientClient = CookBook.BL.Web.MVC.Api.IngredientClient;
using RecipeClient = CookBook.BL.Web.MVC.Api.RecipeClient;

namespace CookBook.BL.Web.MVC.Installers
{
    public class BLWebInstaller : IInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<HttpClient>();
            serviceCollection.AddScoped<IIngredientClient, IngredientClient>();
            serviceCollection.AddScoped<IRecipeClient, RecipeClient>();

            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<BLWebInstaller>()
                .AddClasses(classes => classes.AssignableTo(typeof(IAppFacade)))
                .AsSelf()
                .WithTransientLifetime());
        }
    }
}
