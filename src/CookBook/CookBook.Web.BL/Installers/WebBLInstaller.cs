using System.Net.Http;
using CookBook.Common.BL.Facades;
using CookBook.Common.Installers;
using CookBook.Web.DAL;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Web.BL.Installers
{
    public class WebBLInstaller
    {
        public void Install(IServiceCollection serviceCollection, string apiBaseUrl)
        {
            serviceCollection.AddTransient<IApiClient>(provider => new ApiClient(provider.GetService<HttpClient>(), apiBaseUrl));
            serviceCollection.AddSingleton<LocalDb>();

            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<WebBLInstaller>()
                    .AddClasses(classes => classes.AssignableTo<IAppFacade>())
                    .AsSelfWithInterfaces()
                    .WithTransientLifetime());
        }
    }
}
