using CookBook.BL.Mobile.Services;
using CookBook.Mobile.Views;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Mobile.Installers
{
    public class MobileInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(scan => scan
                .FromAssemblyOf<MobileInstaller>()
                .AddClasses(classes => classes.AssignableTo<ViewBase>())
                    .AsSelf()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo<ISingletonService>())
                    .AsMatchingInterface()
                    .WithSingletonLifetime());
        }
    }
}