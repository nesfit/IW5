using Contacts.MVVM.Framework;
using Contacts.ViewModels.UIServices;
using Contacts.WPF.Windows;
using System;
using System.Threading.Tasks;

namespace Contacts.WPF.UIServices
{
    public class NavigationService : INavigationService
    {
        public void OpenWindow(Window windowKey, object parameter = null)
        {
            var window = CreateWindow(windowKey);
            SetWindow(window, parameter);
            window.Show();
        }

        public Task<bool?> OpenWindowDialog(Window windowKey, object parameter = null)
        {
            var window = CreateWindow(windowKey);
            SetWindow(window, parameter);
            bool? result = window.ShowDialog();
            return Task.FromResult(result);
        }

        private static void SetWindow(MvvmWindow window, object parameter = null)
        {
            window.NavigationParameter = parameter;
        }

        private MvvmWindow CreateWindow(Window window)
        {
            switch (window)
            {
                //case Window.ContactList: return new ContactList();
                case Window.ContactNew: return new ContactNew();
                case Window.ContactEdit: return new ContactEdit();
                default:
                    throw new NotSupportedException();
            }
        }
    }
}