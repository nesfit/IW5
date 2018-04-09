using AutoMapper.QueryableExtensions;
using Contacts.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contacts.DAL
{
    internal class ContactRepository : IContactRepository
    {
        private readonly Func<ContactsDbContext> contactsDbContextFactory;

        public ContactRepository(Func<ContactsDbContext> contactsDbContextFactory)
        {
            this.contactsDbContextFactory = contactsDbContextFactory;
        }

        public async Task<ContactEntity> Delete(ContactEntity contact)
        {
            using (var db = contactsDbContextFactory())
            {
                db.Contacts.Attach(contact);
                db.Contacts.Remove(contact);
                await db.SaveChangesAsync();
                return contact;
            }
        }

        public async Task<T> GetContactById<T>(int id)
        {
            using (var db = contactsDbContextFactory())
            {
                return await db.Contacts.Where(c => c.Id == id).ProjectTo<T>().SingleOrDefaultAsync();
            }
        }

        public async Task<ContactEntity> GetContactById(int id)
        {
            return await GetContactById<ContactEntity>(id);
        }

        public async Task<List<T>> GetContacts<T>(Expression<Func<ContactEntity, bool>> predicate = null)
        {
            predicate = predicate ?? (c => true);
            using (var db = contactsDbContextFactory())
            {
                return await db.Contacts.Where(predicate).ProjectTo<T>().ToListAsync();
            }
        }

        public async Task<List<ContactEntity>> GetContacts(Expression<Func<ContactEntity, bool>> predicate = null)
        {
            return await GetContacts<ContactEntity>(predicate);
        }

        public async Task<ContactEntity> Save(ContactEntity contact)
        {
            using (var db = contactsDbContextFactory())
            {
                db.Contacts.AddOrUpdate(c => c.Id, contact);
                await db.SaveChangesAsync();
                return contact;
            }
        }
    }
}