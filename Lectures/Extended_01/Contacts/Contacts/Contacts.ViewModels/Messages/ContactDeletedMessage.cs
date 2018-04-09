using Contacts.ViewModels.Models;

namespace Contacts.ViewModels.Messages
{
    public class ContactDeletedMessage
    {
        public ContactModel Contact { get; }

        public ContactDeletedMessage(ContactModel contact)
        {
            Contact = contact;
        }
    }
}