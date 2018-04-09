using Contacts.DAL.Entities;

namespace Contacts.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Contacts.DAL.ContactsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Contacts.DAL.ContactsDbContext context)
        {
            context.Contacts.AddOrUpdate(c => c.Id,
                new ContactEntity { Id = 1, Firstname = "Martin", Lastname = "Dybal", Mail = "martin@dybal.it" },
                new ContactEntity { Id = 2, Firstname = "Tereza", Lastname = "Halíková" },
                new ContactEntity { Id = 3, Firstname = "Marie", Lastname = "Marková" },
                new ContactEntity { Id = 4, Firstname = "Tomáš", Lastname = "Pavour" },
                new ContactEntity { Id = 5, Firstname = "Jana", Lastname = "Danielová" },
                new ContactEntity { Id = 6, Firstname = "Vlasta", Lastname = "Otáhalová" },
                new ContactEntity { Id = 7, Firstname = "Josef", Lastname = "Fráněk" },
                new ContactEntity { Id = 8, Firstname = "Jakub", Lastname = "Vedlík" },
                new ContactEntity { Id = 9, Firstname = "Stanislav", Lastname = "Kopečka" },
                new ContactEntity { Id = 10, Firstname = "Karolína ", Lastname = "Černá" },
                new ContactEntity { Id = 11, Firstname = "Hana", Lastname = "Černá" },
                new ContactEntity { Id = 12, Firstname = "Jan", Lastname = "Sychra" },
                new ContactEntity { Id = 13, Firstname = "Milan", Lastname = "Kápl" }
            );
        }
    }
}