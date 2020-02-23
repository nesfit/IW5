using AutoMapper;
using CookBook.Common.Installers;
using CookBook.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.DAL.Installers
{
    public class DALInstaller : IInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<Storage>();

            serviceCollection.Scan(selector =>
                selector.FromCallingAssembly()
                .AddClasses(classes => classes.AssignableTo(typeof(IAppRepository<>)))
                .AsSelfWithInterfaces()
                .WithTransientLifetime()
            );
        }
    }
}