using Contacts.ViewModels.Models;

namespace Contacts.ViewModels.Messages
{
    public class ContactEditedMessage
    {
        public ContactModel Contact { get; }

        public ContactEditedMessage(ContactModel contact)
        {
            Contact = contact;
        }
    }
}