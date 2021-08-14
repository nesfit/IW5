using CookBook.ApiClients;
using CookBook.Common.BL.Facades;
using CookBook.Common.Installers;
using CookBook.Web.DAL;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Web.BL
{
    public class BLWebInstaller : IInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IApiClient, ApiClient>();
            serviceCollection.AddSingleton<LocalDb>();

            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<BLWebInstaller>()
                    .AddClasses(classes => classes.AssignableTo<IAppFacade>())
                    .AsSelfWithInterfaces()
                    .WithTransientLifetime());
        }
    }
}
