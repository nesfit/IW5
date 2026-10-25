using CookBook.Api.DAL.Common.Installers;
using CookBook.Api.DAL.Common.Repositories;
using CookBook.Common.Installers;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Api.DAL.Memory.Installers
{
    public class ApiDALMemoryInstaller : ApiDALInstallerBase
    {
        public override void Install(IServiceCollection serviceCollection)
        {
            base.Install(serviceCollection);

            serviceCollection.AddSingleton<Storage>();

            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<ApiDALMemoryInstaller>()
                        .AddClasses(classes => classes.AssignableTo(typeof(IApiRepository<>)))
                            .AsMatchingInterface()
                            .WithTransientLifetime());
        }
    }
}
