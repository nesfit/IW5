using CookBook.Api.DAL.Common.Repositories;
using CookBook.Api.DAL.Memory.Repositories;
using CookBook.Common.Installers;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Api.DAL.Memory.Installers
{
    public class ApiDALMemoryInstaller : IInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<ApiDALMemoryInstaller>()
                        .AddClasses(classes => classes.AssignableTo(typeof(IApiRepository<>)))
                            .AsMatchingInterface()
                            .WithTransientLifetime()
                        .AddClasses(classes => classes.AssignableTo<Storage>())
                            .AsSelf()
                            .WithSingletonLifetime()
            );

            serviceCollection.AddTransient<RecipeCoreRepository>();
        }
    }
}
