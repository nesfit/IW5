using CookBook.BL.Api.Facades;
using CookBook.Common.Installers;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.BL.Api.Installers
{
    public class BLApiInstaller : IInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<BLApiInstaller>()
                    .AddClasses(filter => filter.AssignableTo<IAppFacade>())
                    .AsSelf()
                    .WithTransientLifetime());
        }
    }
}