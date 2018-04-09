using Contacts.BL;
using Contacts.MVVM.Framework;
using Contacts.ViewModels.Messages;
using Contacts.ViewModels.Models;
using Contacts.ViewModels.UIServices;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Contacts.ViewModels.ViewModels
{
    public class ContactListViewModel : ViewModelBase
    {
        private readonly IContactsService contactsService;
        private readonly INavigationService navigationService;
        private readonly IMessenger messenger;
        private ContactListModel selectedContact;
        public ObservableCollection<ContactListModel> Contacts { get; } = new ObservableCollection<ContactListModel>();

        public ContactListModel SelectedContact
        {
            get => selectedContact;
            set
            {
                selectedContact = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenAddWindowCommand { get; }

        public ICommand OpenEditWindowCommand { get; }

        public ContactListViewModel(IContactsService contactsService, ICommandFactory commandFactory, INavigationService navigationService, IMessenger messenger)
        {
            this.contactsService = contactsService;
            this.navigationService = navigationService;
            this.messenger = messenger;

            OpenAddWindowCommand = commandFactory.CreateCommand(OpenAddWindow);
            OpenEditWindowCommand = commandFactory.CreateCommand(OpenEditWindow, CanOpenEditWindow, this);

            this.messenger.Register<ContactAddedMessage>(action: ContactAddedMessageReceived);
            this.messenger.Register<ContactEditedMessage>(action: ContactEditedMessageReceived);
            this.messenger.Register<ContactDeletedMessage>(action: ContactDeletedMessageReceived);
        }

        protected override async Task LoadData()
        {
            foreach (var contact in await contactsService.GetContacts<ContactListModel>())
            {
                AddContact(contact);
            }

            await base.LoadData();
        }

        private void ContactAddedMessageReceived(ContactAddedMessage message)
        {
            AddContact(message.Contact.MapTo<ContactListModel>());
        }

        private void ContactEditedMessageReceived(ContactEditedMessage message)
        {
            UpdateContact(message.Contact.MapTo<ContactListModel>());
        }

        private void ContactDeletedMessageReceived(ContactDeletedMessage message)
        {
            RemoveContact(message.Contact.Id);
        }

        private bool CanOpenEditWindow()
        {
            return SelectedContact != null;
        }

        private void OpenEditWindow()
        {
            navigationService.OpenWindow(Window.ContactEdit, selectedContact?.Id);
        }

        private void OpenAddWindow()
        {
            navigationService.OpenWindow(Window.ContactNew);
        }
        
        private void AddContact(ContactListModel contact)
        {
            Contacts.Add(contact);
        }

        private void UpdateContact(ContactListModel updatedContact)
        {
            var selectedContactId = SelectedContact.Id;

            var editedContact = Contacts.SingleOrDefault(c => c.Id == updatedContact.Id);
            var editedContactIndex = Contacts.IndexOf(editedContact);
            Contacts[editedContactIndex] = updatedContact;

            if (selectedContactId == updatedContact.Id)
            {
                SelectedContact = updatedContact;
            }
        }

        private void RemoveContact(int contactId)
        {
            Contacts.Remove(Contacts.SingleOrDefault(c => c.Id == contactId));
        }
    }
}