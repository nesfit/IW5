using Contacts.ViewModels.Models;

namespace Contacts.ViewModels.Messages
{
    public class ContactAddedMessage
    {
        public ContactModel Contact { get; }

        public ContactAddedMessage(ContactModel contact)
        {
            Contact = contact;
        }
    }
}