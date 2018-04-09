using Contacts.BL;
using Contacts.MVVM.Framework;
using Contacts.ViewModels.Commands;
using Contacts.ViewModels.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using Contacts.ViewModels.Messages;

namespace Contacts.ViewModels.ViewModels
{
    public class EditContactViewModel : ViewModelBase
    {
        private readonly IContactsService contactsService;
        private readonly IMessenger messenger;
        private ContactModel contact;

        public ContactModel Contact
        {
            get => contact;
            set
            {
                contact = value;
                OnPropertyChanged();
            }
        }

        public ICommand DeleteContactCommand { get; }
        public ICommand SaveContactCommand { get; }

        public EditContactViewModel(IContactsService contactsService, SaveContactCommand saveContactCommand, DeleteContactCommand deleteContactCommand, IMessenger messenger)
        {
            SaveContactCommand = saveContactCommand;
            DeleteContactCommand = deleteContactCommand;
            this.contactsService = contactsService;
            this.messenger = messenger;
            this.messenger.Register<ContactEditedMessage>(action: ContactEditedMessageReceived);
        }

        private void ContactEditedMessageReceived(ContactEditedMessage message)
        {
            if (Contact.Id == message.Contact.Id)
            {
                Contact = message.Contact;
            }
        }

        protected override async Task LoadData()
        {
            if (NavigationParameter is int)
            {
                Contact = await contactsService.GetContactById<ContactModel>((int)NavigationParameter);
            }
        }
    }
}