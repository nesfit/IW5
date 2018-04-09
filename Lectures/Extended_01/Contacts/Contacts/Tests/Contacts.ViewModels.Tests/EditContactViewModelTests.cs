using System.Threading.Tasks;
using Contacts.BL;
using Contacts.MVVM.Framework;
using Contacts.ViewModels.Commands;
using Contacts.ViewModels.Messages;
using Contacts.ViewModels.Models;
using Contacts.ViewModels.ViewModels;
using Moq;
using Xunit;

namespace Contacts.ViewModels.Tests
{
    public class EditContactViewModelTests
    {
        [Fact]
        public void SetContact_NavigationParameterAtLoad_ContactLoadedFromStore()
        {
            var contactsServiceMock = new Mock<IContactsService>();
            var selectedContactId = 1;
            var selectedContactModel = new ContactModel { Id = selectedContactId, Firstname = "Martin", Lastname = "Dybal" };
            contactsServiceMock.Setup(s => s.GetContactById<ContactModel>(selectedContactId)).Returns(Task.FromResult(selectedContactModel));
            var editContactViewModelSUT = CreateEditContactViewModel(contactsServiceMock.Object);

            editContactViewModelSUT.NavigationParameter = selectedContactId;
            editContactViewModelSUT.StartLoadData();

            contactsServiceMock.Verify(x => x.GetContactById<ContactModel>(selectedContactId), Times.Once());
            Assert.Equal(selectedContactModel.Id, editContactViewModelSUT.Contact?.Id);
        }

        [Fact]
        public async Task SetContact_ContactEditedMessageReceived_SelectedContactSet()
        {
            var messenger = new Messenger();
            var editContactViewModelSUT = CreateEditContactViewModel(messenger);
            var selectedContactModelId = editContactViewModelSUT.Contact.Id;
            var selectedContactModel = new ContactModel { Id = selectedContactModelId, Firstname = "Martin", Lastname = "Dybal" };

            await messenger.Send(new ContactEditedMessage(selectedContactModel));

            Assert.Equal(selectedContactModel, editContactViewModelSUT.Contact);
        }

        private EditContactViewModel CreateEditContactViewModel(IMessenger messenger)
        {
            return CreateEditContactViewModel(null, messenger);
        }

        private EditContactViewModel CreateEditContactViewModel(IContactsService contactsService = null, IMessenger messenger = null)
        {
            contactsService = contactsService ?? new Mock<IContactsService>().Object;
            messenger = messenger ?? new Mock<IMessenger>().Object;

            var saveContactCommand = new SaveContactCommand(contactsService, messenger);
            var deleteContactCommand = new DeleteContactCommand(contactsService, messenger);

            return new EditContactViewModel(contactsService, saveContactCommand, deleteContactCommand, messenger)
            {
                Contact = new ContactModel
                {
                    Id = 1,
                    Firstname = "Martin",
                    Lastname = "Dybal",
                    Mail = "martin@dybal.it"
                }
            };
        }
    }
}