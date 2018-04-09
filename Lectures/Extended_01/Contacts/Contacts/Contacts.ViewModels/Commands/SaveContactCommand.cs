using Contacts.BL;
using Contacts.MVVM.Framework;
using Contacts.ViewModels.Messages;
using Contacts.ViewModels.Models;
using System;
using System.Windows.Input;

namespace Contacts.ViewModels.Commands
{
    public class SaveContactCommand : ICommand
    {
        private readonly IContactsService contactsService;
        private readonly IMessenger messenger;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public SaveContactCommand(IContactsService contactsService, IMessenger messenger)
        {
            this.contactsService = contactsService;
            this.messenger = messenger;
        }

        public bool CanExecute(object parameter)
        {
            return parameter is ContactModel;
        }

        public async void Execute(object parameter)
        {
            if (parameter is ContactModel contact)
            {
                contact = await contactsService.Save(contact);
                await messenger.Send(new ContactEditedMessage(contact));
            }
        }
    }
}