using Contacts.BL;
using Contacts.MVVM.Framework;
using Contacts.ViewModels.Commands;
using Contacts.ViewModels.Messages;
using Contacts.ViewModels.Models;
using Moq;
using Xunit;

namespace Contacts.ViewModels.Tests
{
    public class DeleteContactCommandTests
    {
        [Fact]
        public void CanExecute_ParameterContactModel_True()
        {
            var deleteContactCommand = CreateDeleteContactCommand();

            var canExecute = deleteContactCommand.CanExecute(new ContactModel());

            Assert.True(canExecute);
        }

        [Fact]
        public void CanExecute_ParameterNull_False()
        {
            var deleteContactCommand = CreateDeleteContactCommand();

            var canExecute = deleteContactCommand.CanExecute(null);

            Assert.False(canExecute);
        }

        [Fact]
        public void Execute_ParameterContactModel_DeleteFromStore()
        {
            var contactsServiceMock = new Mock<IContactsService>();
            var deleteContactCommand = CreateDeleteContactCommand(contactsServiceMock.Object);
            var contact = CreateContactModel();

            deleteContactCommand.TryExecute(contact);

            contactsServiceMock.Verify(x => x.Delete(It.IsAny<ContactModel>()), Times.Once());
        }

        [Fact]
        public void Execute_ParameterNull_NotDeleteFromStore()
        {
            var contactsServiceMock = new Mock<IContactsService>();
            var deleteContactCommand = CreateDeleteContactCommand(contactsServiceMock.Object);

            deleteContactCommand.Execute(null);

            contactsServiceMock.Verify(x => x.Delete(It.IsAny<ContactModel>()), Times.Never());
        }

        [Fact]
        public void Execute_ParameterContactModel_SendsDeletedMessage()
        {
            var messengerMock = new Mock<IMessenger>();
            var deleteContactCommand = CreateDeleteContactCommand(messengerMock.Object);
            var contact = CreateContactModel();

            deleteContactCommand.Execute(contact);

            messengerMock.Verify(x => x.Send(It.IsAny<ContactDeletedMessage>()), Times.Once());
        }

        [Fact]
        public void Execute_ParameterNull_NotSendsDeletedMessage()
        {
            var messengerMock = new Mock<IMessenger>();
            var deleteContactCommand = CreateDeleteContactCommand(messengerMock.Object);

            deleteContactCommand.Execute(null);

            messengerMock.Verify(x => x.Send(It.IsAny<ContactDeletedMessage>()), Times.Never);
        }

        private static ContactModel CreateContactModel()
        {
            var contact = new ContactModel
            {
                Id = 1,
                Firstname = "Martin",
                Lastname = "Dybal",
                Mail = "martin@dybal.it"
            };
            return contact;
        }

        private DeleteContactCommand CreateDeleteContactCommand(IMessenger messenger)
        {
            return CreateDeleteContactCommand(null, messenger);
        }

        private DeleteContactCommand CreateDeleteContactCommand(IContactsService contactsService = null, IMessenger messenger = null)
        {
            contactsService = contactsService ?? new Mock<IContactsService>().Object;
            messenger = messenger ?? new Mock<IMessenger>().Object;
            return new DeleteContactCommand(contactsService, messenger);
        }
    }
}