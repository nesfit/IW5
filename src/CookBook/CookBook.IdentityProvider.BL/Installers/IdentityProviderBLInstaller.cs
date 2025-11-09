using CookBook.Common.BL.Facades;
using CookBook.Common.Installers;
using CookBook.IdentityProvider.BL.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.IdentityProvider.BL.Installers;

public class IdentityProviderBLInstaller : IInstaller
{
    public void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<AppUserClaimMapper>();
        serviceCollection.AddSingleton<AppUserMapper>();

        serviceCollection.Scan(selector =>
            selector.FromAssemblyOf<IdentityProviderBLInstaller>()
                .AddClasses(classes => classes.AssignableTo<IAppFacade>())
                .AsSelfWithInterfaces()
                .WithScopedLifetime());
    }
}
