using CookBook.Api.DAL.Common.Installers;
using CookBook.Api.DAL.Common.Mappers;
using CookBook.Api.DAL.Common.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Api.DAL.EF.Installers
{
    public class ApiDALEFInstaller : ApiDALInstallerBase
    {
        public void Install(IServiceCollection serviceCollection, string connectionString)
        {
            base.Install(serviceCollection);

            serviceCollection.AddDbContext<CookBookDbContext>(options => options.UseSqlServer(connectionString));

            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<ApiDALEFInstaller>()
                    .AddClasses(classes => classes.AssignableTo(typeof(IApiRepository<>)))
                    .AsMatchingInterface()
                    .WithScopedLifetime());
        }
    }
}
