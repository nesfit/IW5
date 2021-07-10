using CookBook.Common.Installers;
using CookBook.DAL.Api.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.DAL.Api.Installers
{
    public class DALInstaller : IInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<DALInstaller>()
                        .AddClasses(classes => classes.AssignableTo(typeof(IApiRepository<>)))
                            .AsSelfWithInterfaces()
                            .WithTransientLifetime()
                        .AddClasses(classes => classes.AssignableTo<Storage>())
                            .AsSelf()
                            .WithSingletonLifetime()
            );
        }
    }
}