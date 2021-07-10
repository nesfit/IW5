using CookBook.ApiClients;
using CookBook.BL.Common.Facades;
using CookBook.Common.Installers;
using CookBook.DAL.Web;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.BL.Web
{
    public class BLWebInstaller : IInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IIngredientClient, IngredientClient>();
            serviceCollection.AddTransient<IRecipeClient, RecipeClient>();
            serviceCollection.AddSingleton<LocalDb>();

            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<BLWebInstaller>()
                    .AddClasses(classes => classes.AssignableTo<IAppFacade>())
                    .AsSelfWithInterfaces()
                    .WithTransientLifetime());
        }
    }
}