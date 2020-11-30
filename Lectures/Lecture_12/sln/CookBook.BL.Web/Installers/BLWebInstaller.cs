using CookBook.BL.Common.Facades;
using CookBook.BL.Web.Api;
using CookBook.Common.Installers;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.BL.Web.Installers
{
    public class BLWebInstaller : IInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IIngredientClient, IngredientClient>();
            serviceCollection.AddTransient<IRecipeClient, RecipeClient>();

            serviceCollection.Scan(selector =>
                selector.FromCallingAssembly()
                    .AddClasses(classes => classes.AssignableTo<IAppFacade>())
                    .AsSelfWithInterfaces()
                    .WithTransientLifetime());
        }
    }
}