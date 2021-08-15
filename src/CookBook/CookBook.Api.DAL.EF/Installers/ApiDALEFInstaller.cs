using System;
using CookBook.Api.DAL.Common.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Api.DAL.EF.Installers
{
    public class ApiDALEFInstaller
    {
        public void Install(IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<CookBookDbContext>(options => options.UseSqlServer(connectionString));
            serviceCollection.AddTransient<Func<CookBookDbContext>>(provider => () => provider.GetService<CookBookDbContext>()!);

            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<ApiDALEFInstaller>()
                    .AddClasses(classes => classes.AssignableTo(typeof(IApiRepository<>)))
                    .AsMatchingInterface()
                    .WithTransientLifetime());
        }
    }
}
