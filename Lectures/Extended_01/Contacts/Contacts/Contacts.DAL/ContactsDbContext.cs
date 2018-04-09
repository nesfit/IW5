using Contacts.DAL.Entities;
using System.Data.Entity;

namespace Contacts.DAL
{
    internal class ContactsDbContext : DbContext
    {
        public DbSet<ContactEntity> Contacts { get; set; }

        public ContactsDbContext() : base("ContactsDb")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactEntity>().ToTable(nameof(Contacts));
            base.OnModelCreating(modelBuilder);
        }
    }
}