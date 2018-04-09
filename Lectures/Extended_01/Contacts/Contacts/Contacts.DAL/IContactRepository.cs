using Contacts.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contacts.DAL
{
    public interface IContactRepository
    {
        Task<ContactEntity> Delete(ContactEntity contact);

        Task<T> GetContactById<T>(int id);

        Task<ContactEntity> GetContactById(int id);

        Task<List<T>> GetContacts<T>(Expression<Func<ContactEntity, bool>> predicate = null);

        Task<List<ContactEntity>> GetContacts(Expression<Func<ContactEntity, bool>> predicate = null);

        Task<ContactEntity> Save(ContactEntity contact);
    }
}