using CookBook.BL.Common.Facades;
using CookBook.BL.Web.Blazor.Api;
using CookBook.Common.Installers;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.BL.Web.Blazor.Installers
{
    public class BLWebBlazorInstaller : IInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IIngredientClient, IngredientClient>();
            serviceCollection.AddScoped<IRecipeClient, RecipeClient>();
            serviceCollection.AddScoped<ILoggingClient, LoggingClient>();

            serviceCollection.Scan(scan =>
                scan.FromAssemblyOf<BLWebBlazorInstaller>()
                    .AddClasses(classes => classes.AssignableTo<IAppFacade>())
                    .AsMatchingInterface());
        }
    }
}