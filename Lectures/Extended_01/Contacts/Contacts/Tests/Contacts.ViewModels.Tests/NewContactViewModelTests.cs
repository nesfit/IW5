using Contacts.BL;
using Contacts.MVVM.Framework;
using Contacts.ViewModels.Messages;
using Contacts.ViewModels.Models;
using Contacts.ViewModels.ViewModels;
using Moq;
using Xunit;

namespace Contacts.ViewModels.Tests
{
    public class NewContactViewModelTests
    {
        [Fact]
        public void AddContactCommand_Execute_AddToStore()
        {
            var contactsServiceMock = new Mock<IContactsService>();
            var newContactViewModelSUT = CreateNewContactViewModel(contactsServiceMock.Object);

            newContactViewModelSUT.AddContactCommand.TryExecute(null);

            contactsServiceMock.Verify(x => x.Save(It.IsAny<ContactModel>()), Times.Once());
        }

        [Fact]
        public void AddContactCommand_Execute_SendsAddedMessage()
        {
            var messengerMock = new Mock<IMessenger>();
            var newContactViewModelSUT = CreateNewContactViewModel(messengerMock.Object);

            newContactViewModelSUT.AddContactCommand.TryExecute(null);

            messengerMock.Verify(x => x.Send(It.IsAny<ContactAddedMessage>()), Times.Once());
        }

        private NewContactViewModel CreateNewContactViewModel(IMessenger messenger)
        {
            return CreateNewContactViewModel(null, messenger);
        }

        private NewContactViewModel CreateNewContactViewModel(IContactsService contactsService = null, IMessenger messenger = null)
        {
            contactsService = contactsService ?? new Mock<IContactsService>().Object;
            messenger = messenger ?? new Mock<IMessenger>().Object;

            var commandFactory = new CommandFactory();
            return new NewContactViewModel(contactsService, messenger, commandFactory)
            {
                Contact =
                {
                    Firstname = "Martin",
                    Lastname = "Dybal",
                    Mail = "martin@dybal.it"
                }
            };
        }
    }
}