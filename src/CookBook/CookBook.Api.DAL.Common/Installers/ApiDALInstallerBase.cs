using CookBook.Api.DAL.Common.Mappers;
using CookBook.Common.Installers;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Api.DAL.Common.Installers
{
    public abstract class ApiDALInstallerBase : IInstaller
    {
        public virtual void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<RecipeMapper>();
        }
    }
}
