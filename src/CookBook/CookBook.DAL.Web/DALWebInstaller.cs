using CookBook.Common.Installers;
using CookBook.DAL.Web.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.DAL.Web
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