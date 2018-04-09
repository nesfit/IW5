using Castle.Windsor;
using Contacts.MVVM.Framework.WindsorInstaller;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Contacts.MVVM.Framework.Tests
{
    public class MessengerTests
    {
        private readonly Action<object> action;
        private readonly Func<object, Task> actionTask;
        private int messageReceivedCount;

        public MessengerTests()
        {
            actionTask = message =>
            {
                messageReceivedCount++;
                return Task.FromResult(0);
            };

            action = message => messageReceivedCount++;
        }

        [Fact]
        public async Task Register_Test()
        {
            messageReceivedCount = 0;

            var messengerSUT = CreateMessenger();

            messengerSUT.Register(action);
            await messengerSUT.Send(new object());

            Assert.Equal(1, messageReceivedCount);
        }

        [Fact]
        public async Task RegisterAsync_Test()
        {
            messageReceivedCount = 0;

            var messengerSUT = CreateMessenger();

            messengerSUT.Register(actionTask);
            await messengerSUT.Send(new object());

            Assert.Equal(1, messageReceivedCount);
        }

        [Fact]
        public async Task RegisterAsyncTwice_Test()
        {
            messageReceivedCount = 0;

            var messengerSUT = CreateMessenger();

            messengerSUT.Register(actionTask);
            messengerSUT.Register(actionTask);
            await messengerSUT.Send(new object());

            Assert.Equal(2, messageReceivedCount);
        }

        [Fact]
        public async Task RegisterTwice_Test()
        {
            messageReceivedCount = 0;

            var messengerSUT = CreateMessenger();

            messengerSUT.Register(action);
            messengerSUT.Register(action);
            await messengerSUT.Send(new object());

            Assert.Equal(2, messageReceivedCount);
        }

        [Fact]
        public async Task UnRegister_Test()
        {
            var messageReceivedCount = 0;
            var messengerSUT = CreateMessenger();

            messengerSUT.Register(action);
            messengerSUT.UnRegister(action);
            await messengerSUT.Send(new object());

            Assert.Equal(0, messageReceivedCount);
        }

        [Fact]
        public async Task UnRegisterAsync_Test()
        {
            var messageReceivedCount = 0;
            var messengerSUT = CreateMessenger();

            messengerSUT.Register(actionTask);
            messengerSUT.UnRegister(actionTask);
            await messengerSUT.Send(new object());

            Assert.Equal(0, messageReceivedCount);
        }

        private IMessenger CreateMessenger()
        {
            var container = new WindsorContainer();
            container.Install(new MVVMFrameworkInstaller());
            return container.Resolve<IMessenger>();
        }
    }
}