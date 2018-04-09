using System.Windows;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Contacts.ViewModels;
using Contacts.ViewModels.UIServices;
using Contacts.ViewModels.WindsorInstaller;
using Contacts.WPF.UIServices;
using Contacts.WPF.Windows;

namespace Contacts.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
private readonly WindsorContainer container = new WindsorContainer();
        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            MapperConfig.MapperConfig.Initialize();
            ContainerBootstrapper();
            Resources["ViewModelLocator"] = container.Resolve<ViewModelLocator>();
            var mainView = container.Resolve<ContactList>();
            mainView.Show();
        }

        private void ContainerBootstrapper()
        {
            container.Register(Component.For<INavigationService>().ImplementedBy<NavigationService>().LifestyleTransient());
            container.Install(new ViewModelsInstaller());

            container.Register(Component.For<ContactList>().LifestyleTransient());
        }
    }
}