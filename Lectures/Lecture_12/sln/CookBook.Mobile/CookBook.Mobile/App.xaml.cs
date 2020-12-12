using CookBook.BL.Mobile.Installers;
using CookBook.BL.Mobile.Services;
using CookBook.BL.Mobile.ViewModels;
using CookBook.Common.Installers;
using CookBook.Mobile.Installers;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CookBook.Mobile
{
    public partial class App : Application
    {
        public IDependencyInjectionService DependencyInjectionService { get; }
        public App(IEnumerable<IInstaller> installers = null)
        {
            InitializeComponent();
            DependencyInjectionService = new DependencyInjectionService();

            var navigationPage = new NavigationPage();
            RegisterDependencies(DependencyInjectionService, navigationPage.Navigation, installers);

            var navigationService = DependencyInjectionService.Resolve<INavigationService>();
            navigationService.PushAsync<HomeViewModel>();

            MainPage = navigationPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void RegisterDependencies(IDependencyInjectionService dependencyInjectionService, INavigation navigation, IEnumerable<IInstaller> installers = null)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(dependencyInjectionService);
            serviceCollection.AddSingleton(navigation);

            var blMobileInstaller = new BLMobileInstaller();
            blMobileInstaller.Install(serviceCollection);

            var mobileInstaller = new MobileInstaller();
            mobileInstaller.Install(serviceCollection);

            if (installers != null)
            {
                foreach (var installer in installers)
                {
                    installer.Install(serviceCollection);
                }
            }

            dependencyInjectionService.Build(serviceCollection);
        }
    }
}
