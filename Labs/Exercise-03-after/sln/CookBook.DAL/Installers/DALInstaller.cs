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
                selector.FromAssemblyOf<DALInstaller>()
                    .AddClasses(filter => filter.AssignableTo(typeof(IAppRepository<>)))
                    .AsSelf()
                    .WithTransientLifetime());
        }
    }
}