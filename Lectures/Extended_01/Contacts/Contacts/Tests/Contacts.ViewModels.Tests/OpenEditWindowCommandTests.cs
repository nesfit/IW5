using Contacts.ViewModels.Commands;
using Contacts.ViewModels.Models;
using Contacts.ViewModels.UIServices;
using Moq;
using Xunit;

namespace Contacts.ViewModels.Tests
{
    public class OpenEditWindowCommandTests
    {
        [Fact]
        public void OpenEditWindowCommand_CanExecute_IsTrueWhenParameterIsInt_Test()
        {
            var openEditWindowCommandSUT = CreateOpenEditWindowCommand();
            var contact = new ContactListModel
            {
                Id = 1
            };

            var canExecute = openEditWindowCommandSUT.CanExecute(contact);

            Assert.True(canExecute);
        }

        [Fact]
        public void OpenEditWindowCommand_CanExecute_IsFalseWhenParameterIsNotInt_Test()
        {
            var openEditWindowCommandSUT = CreateOpenEditWindowCommand();

            var canExecute = openEditWindowCommandSUT.CanExecute(null);

            Assert.False(canExecute);
        }

        [Fact]
        public void OpenEditWindowCommand_Execute_IsFalseWhenParameterIsNotInt_Test()
        {
            var navigationServiceMock = new Mock<INavigationService>();
            var openEditWindowCommandSUT = CreateOpenEditWindowCommand(navigationServiceMock.Object);
            var contact = new ContactListModel
            {
                Id = 1
            };

            openEditWindowCommandSUT.TryExecute(contact);

            navigationServiceMock.Verify(x => x.OpenWindow(Window.ContactEdit, It.IsAny<int>()), Times.Once());
        }

        private OpenEditWindowCommand CreateOpenEditWindowCommand(INavigationService navigationService = null)
        {
            navigationService = navigationService ?? new Mock<INavigationService>().Object;
            return new OpenEditWindowCommand(navigationService);
        }
    }
}