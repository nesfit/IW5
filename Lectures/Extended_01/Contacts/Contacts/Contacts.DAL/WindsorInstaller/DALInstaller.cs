using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;

namespace Contacts.DAL.WindsorInstaller
{
    public class DALInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<Func<ContactsDbContext>>().Instance(() => new ContactsDbContext()));
            container.Register(Component.For<IContactRepository>().ImplementedBy<ContactRepository>().LifestyleTransient());
        }
    }
}