using CookBook.Common.Options;
using CookBook.Web.App.Options;
using CookBook.Web.BL.Installers;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Web.BL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInstaller<TInstaller>(
            this IServiceCollection serviceCollection,
            IdentityOptions identityOptions,
            WebAuthenticationProviderOptions? selectedProvider,
            ApiOptions apiOptions)
            where TInstaller : WebBLInstaller, new()
        {
            var installer = new TInstaller();
            installer.Install(serviceCollection, identityOptions, selectedProvider, apiOptions);
        }
    }
}
