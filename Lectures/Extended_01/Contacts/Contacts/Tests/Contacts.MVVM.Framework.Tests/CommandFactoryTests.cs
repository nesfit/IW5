using System;
using Xunit;

namespace Contacts.MVVM.Framework.Tests
{
    public class CommandFactoryTests
    {
        [Fact]
        public void CreateCommandWithoutCanExecute_Test()
        {
            var commandFactory = new CommandFactory();

            bool wasExecuteCall = false;
            Action execute = () => wasExecuteCall = true;
            var command = commandFactory.CreateCommand(execute);

            command.Execute(null);

            Assert.True(wasExecuteCall);
            Assert.True(command.CanExecute(null));
        }

        [Fact]
        public void CreateCommandWithCanExecute_Test()
        {
            var commandFactory = new CommandFactory();

            bool wasExecuteCall = false;
            Action execute = () => wasExecuteCall = true;
            Func<bool> canExecute = () => true;
            var command = commandFactory.CreateCommand(execute, canExecute);

            command.Execute(null);
            Assert.True(wasExecuteCall);

            Assert.True(command.CanExecute(null));
        }
    }
}