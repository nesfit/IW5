using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Contacts.MVVM.Framework.WindsorInstaller
{
    public class MVVMFrameworkInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ICommandFactory>().ImplementedBy<CommandFactory>().LifestyleTransient());
            container.Register(Component.For<IMessenger>().ImplementedBy<Messenger>().LifestyleSingleton());
        }
    }
}