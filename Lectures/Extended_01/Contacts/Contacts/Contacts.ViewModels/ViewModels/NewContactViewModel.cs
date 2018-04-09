using Contacts.BL;
using Contacts.MVVM.Framework;
using Contacts.ViewModels.Messages;
using Contacts.ViewModels.Models;
using System.Windows.Input;

namespace Contacts.ViewModels.ViewModels
{
    public class NewContactViewModel : ViewModelBase
    {
        private readonly IContactsService contactsService;
        private readonly IMessenger messenger;
        public ICommand AddContactCommand { get; }

        public ContactModel Contact { get; private set; } = new ContactModel();

        public NewContactViewModel(IContactsService contactsService, IMessenger messenger, ICommandFactory commandFactory)
        {
            this.contactsService = contactsService;
            this.messenger = messenger;
            AddContactCommand = commandFactory.CreateCommand(AddContact);
        }

        private async void AddContact()
        {
            Contact = await contactsService.Save(Contact);
            await messenger.Send(new ContactAddedMessage(Contact));
        }
    }
}