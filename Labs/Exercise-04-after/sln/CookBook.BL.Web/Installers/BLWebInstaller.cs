using CookBook.BL.Common.Facades;
using CookBook.BL.Web.Api;
using CookBook.Common.Installers;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace CookBook.BL.Web.Installers
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
