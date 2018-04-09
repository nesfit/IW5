using Contacts.BL;
using Contacts.MVVM.Framework;
using Contacts.ViewModels.Messages;
using Contacts.ViewModels.Models;
using System;
using System.Windows.Input;

namespace Contacts.ViewModels.Commands
{
    public class DeleteContactCommand : ICommand
    {
        private readonly IContactsService contactsService;
        private readonly IMessenger messenger;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public DeleteContactCommand(IContactsService contactsService, IMessenger messenger)
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
                await contactsService.Delete(contact);
                await messenger.Send(new ContactDeletedMessage(contact));
            }
        }
    }
}