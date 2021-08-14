using CookBook.Common.Installers;
using CookBook.Web.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Web.DAL
{
    public class DALWebInstaller : IInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<LocalDb>();
            serviceCollection.Scan(scan =>
                scan.FromAssemblyOf<DALWebInstaller>()
                    .AddClasses(classes => classes.AssignableTo(typeof(IWebRepository<>)))
                    .AsSelf()
                    .WithSingletonLifetime());
        }
    }
}