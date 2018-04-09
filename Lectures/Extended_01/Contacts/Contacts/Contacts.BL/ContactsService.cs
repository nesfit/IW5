using AutoMapper;
using Contacts.DAL;
using Contacts.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contacts.BL
{
    internal class ContactsService : IContactsService
    {
        private readonly IContactRepository contactRepository;

        public ContactsService(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public async Task<T> Delete<T>(T contact)
        {
            var dbContact = await contactRepository.Delete(contact.MapTo<ContactEntity>());
            return dbContact.MapTo<T>();
        }

        public async Task<T> GetContactById<T>(int id)
        {
            return await contactRepository.GetContactById<T>(id);
        }

        public async Task<IList<T>> GetContacts<T>()
        {
            return await contactRepository.GetContacts<T>();
        }

        public async Task<T> Save<T>(T contact)
        {
            var dbContact = await contactRepository.Save(contact.MapTo<ContactEntity>());
            return dbContact.MapTo<T>();
        }
    }
}