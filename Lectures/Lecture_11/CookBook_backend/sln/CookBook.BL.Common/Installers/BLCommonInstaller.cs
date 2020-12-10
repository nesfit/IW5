using CookBook.BL.Common.Services;
using CookBook.Common.Installers;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.BL.Common.Installers
{
    public class BLCommonInstaller : IInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(scan =>
                scan.FromAssemblyOf<BLCommonInstaller>()
                    .AddClasses(classes => classes.AssignableTo<IAppService>())
                    .AsMatchingInterface()
                    .WithSingletonLifetime());
        }
    }
}