using CookBook.Common.BL.Facades;
using CookBook.Common.Installers;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Api.BL.Installers
{
    public class ApiBLInstaller : IInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<ApiBLInstaller>()
                        .AddClasses(classes => classes.AssignableTo<IAppFacade>())
                        .AsSelfWithInterfaces()
                        .WithScopedLifetime());
        }
    }
}
