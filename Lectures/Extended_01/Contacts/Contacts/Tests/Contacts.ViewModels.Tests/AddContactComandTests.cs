using Contacts.BL;
using Contacts.MVVM.Framework;
using Contacts.ViewModels.Commands;
using Contacts.ViewModels.Messages;
using Contacts.ViewModels.Models;
using Moq;
using Xunit;

namespace Contacts.ViewModels.Tests
{
    public class AddContactComandTests
    {

        [Fact]
        public void CanExecute_ParameterContactModel_True()
        {
            var saveContactCommandSUT = CreateSaveContactCommandSUT();
            var parameter = CreateTestContact();

            var canExecute = saveContactCommandSUT.CanExecute(parameter);

            Assert.True(canExecute);
        }

        [Fact]
        public void CanExecute_ParameterNull_False()
        {
            var saveContactCommandSUT = CreateSaveContactCommandSUT();

            var canExecute = saveContactCommandSUT.CanExecute(null);

            Assert.False(canExecute);
        }

        [Fact]
        public void Execute_ParameterContactModel_SendsAddedMessage()
        {
            var messengerMock = new Mock<IMessenger>();
            var saveContactCommandSUT = CreateSaveContactCommandSUT(messengerMock.Object);
            var parameter = CreateTestContact();

            saveContactCommandSUT.TryExecute(parameter);

            messengerMock.Verify(x => x.Send(It.IsAny<ContactEditedMessage>()), Times.Once());
        }

        [Fact]
        public void Execute_ParameterNull_NotSendsAddedMessage()
        {
            var messengerMock = new Mock<IMessenger>();
            var saveContactCommandSUT = CreateSaveContactCommandSUT(messengerMock.Object);
            
            saveContactCommandSUT.TryExecute(null);

            messengerMock.Verify(x => x.Send(It.IsAny<ContactEditedMessage>()), Times.Never);
        }

        [Fact]
        public void Execute_ParameterContactModel_SavesToStore()
        {
            var contactsServiceMock = new Mock<IContactsService>();
            var saveContactCommandSUT = CreateSaveContactCommandSUT(contactsServiceMock.Object);
            var parameter = CreateTestContact();

            saveContactCommandSUT.TryExecute(parameter);

            contactsServiceMock.Verify(x => x.Save(It.IsAny<ContactModel>()), Times.Once());
        }

        [Fact]
        public void Execute_ParameterNull_NotSavesToStore()
        {
            var contactsServiceMock = new Mock<IContactsService>();
            var saveContactCommandSUT = CreateSaveContactCommandSUT(contactsServiceMock.Object);

            saveContactCommandSUT.TryExecute(null);

            contactsServiceMock.Verify(x => x.Save(It.IsAny<ContactModel>()), Times.Never);
        }

        private SaveContactCommand CreateSaveContactCommandSUT(IMessenger messenger)
        {
            return CreateSaveContactCommandSUT(null, messenger);
        }

        private SaveContactCommand CreateSaveContactCommandSUT(IContactsService contactsService = null, IMessenger messenger = null)
        {
            contactsService = contactsService ?? new Mock<IContactsService>().Object;
            messenger = messenger ?? new Mock<IMessenger>().Object;

            return new SaveContactCommand(contactsService, messenger);
        }

        private ContactModel CreateTestContact()
        {
            return new ContactModel
            {
                Id = 1,
                Firstname = "Martin",
                Lastname = "Dybal",
                Mail = "martin@dybal.it"
            };
        }
    }
}