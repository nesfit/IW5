using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Contacts.DAL.WindsorInstaller;

namespace Contacts.BL.WindsorInstaller
{
    public class BLInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Install(new DALInstaller());
            container.Register(Component.For<IContactsService>().ImplementedBy<ContactsService>().LifestyleTransient());
        }
    }
}