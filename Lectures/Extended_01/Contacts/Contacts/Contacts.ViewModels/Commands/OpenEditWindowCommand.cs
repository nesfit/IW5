using Contacts.ViewModels.Models;
using Contacts.ViewModels.UIServices;
using System;
using System.Windows.Input;

namespace Contacts.ViewModels.Commands
{
    public class OpenEditWindowCommand : ICommand
    {
        private readonly INavigationService navigationService;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public OpenEditWindowCommand(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public bool CanExecute(object parameter)
        {
            return GetContactId(parameter) != null;
        }

        public void Execute(object parameter)
        {
            var selectedContactId = GetContactId(parameter);
            if (selectedContactId.HasValue)
            {
                navigationService.OpenWindow(Window.ContactEdit, selectedContactId);
            }
        }

        private static int? GetContactId(object parameter)
        {
            return (parameter as ContactListModel)?.Id;
        }
    }
}