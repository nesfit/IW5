using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contacts.BL
{
    public interface IContactsService
    {
        Task<T> Delete<T>(T contact);

        Task<T> GetContactById<T>(int id);

        Task<IList<T>> GetContacts<T>();

        Task<T> Save<T>(T contact);
    }
}