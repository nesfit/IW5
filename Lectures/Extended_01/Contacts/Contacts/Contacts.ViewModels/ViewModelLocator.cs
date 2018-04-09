using System;
using Contacts.ViewModels.ViewModels;

namespace Contacts.ViewModels
{
    public class ViewModelLocator
    {
        private readonly Func<ContactListViewModel> contactListViewModelFactoryy;
        private readonly Func<EditContactViewModel> editContactViewModelFactory;
        private readonly Func<NewContactViewModel> newContactViewModelFactory;

        public ContactListViewModel ContactListViewModel => contactListViewModelFactoryy();
        public EditContactViewModel EditContactViewModel => editContactViewModelFactory();
        public NewContactViewModel NewContactViewModel => newContactViewModelFactory();

        public ViewModelLocator(Func<ContactListViewModel> contactListViewModelFactoryy, Func<EditContactViewModel> editContactViewModelFactory, Func<NewContactViewModel> newContactViewModelFactory)
        {
            this.contactListViewModelFactoryy = contactListViewModelFactoryy;
            this.editContactViewModelFactory = editContactViewModelFactory;
            this.newContactViewModelFactory = newContactViewModelFactory;
        }
    }
}