using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Contacts.BL.WindsorInstaller;
using Contacts.MVVM.Framework;
using Contacts.MVVM.Framework.WindsorInstaller;
using Contacts.ViewModels.ViewModels;
using System;
using System.Windows.Input;

namespace Contacts.ViewModels.WindsorInstaller
{
    public class ViewModelsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Install(new BLInstaller());
            container.Install(new MVVMFrameworkInstaller());

            container.Register(Classes.FromThisAssembly().BasedOn<ICommand>().LifestyleTransient());

            container.Register(Classes.FromThisAssembly().BasedOn<ViewModelBase>().LifestyleTransient());

            RegisterViewModelFactory<ContactListViewModel>(container);
            RegisterViewModelFactory<EditContactViewModel>(container);
            RegisterViewModelFactory<NewContactViewModel>(container);

            container.Register(Component.For<ViewModelLocator>().LifestyleTransient());
        }

        private static void RegisterViewModelFactory<T>(IWindsorContainer container)
        {
            var viewModelComponent = Component.For<Func<T>>()
                                              .Instance(container.Resolve<T>)
                                              .LifestyleTransient();
            container.Register(viewModelComponent);
        }
    }
}