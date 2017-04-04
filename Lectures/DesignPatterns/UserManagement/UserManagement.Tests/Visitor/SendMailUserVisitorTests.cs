using System;
using Moq;
using UserManagement.ConsoleApp.Composite;
using UserManagement.ConsoleApp.Emails;
using UserManagement.ConsoleApp.Repository;
using UserManagement.ConsoleApp.Visitor;
using Xunit;

namespace UserManagement.Tests.Visitor
{
    public class SendMailUserVisitorTests
    {
        [Fact]
        public void SendEmail_Group_SendsEmailToAllInGroup()
        {
            var email = new Email("test", "test body");
            var mailerServiceMock = new Mock<IMailerService>();
            mailerServiceMock.Setup(mailerService => mailerService.SendEmail(It.IsAny<string>(), email));
            SendMailUserVisitor sendMailUserVisitorSUT = new SendMailUserVisitor(mailerServiceMock.Object);

            var bob = new UserComponent { Name = "Bob", Email = "bob@super-company.com"};
            var developers = new UserGroupComposite
            {
                Name = "Developers",
                Members = { bob }
            };

            sendMailUserVisitorSUT.SendEmail(developers, email);

            mailerServiceMock.Verify(mailerService => mailerService.SendEmail(It.IsAny<string>(), email), Times.Once);
        }

        [Fact]
        public void SendEmail_User_SendsEmailToUser()
        {
            var email = new Email("test", "test body");
            var mailerServiceMock = new Mock<IMailerService>();
            mailerServiceMock.Setup(mailerService => mailerService.SendEmail(It.IsAny<string>(), email));
            SendMailUserVisitor sendMailUserVisitorSUT = new SendMailUserVisitor(mailerServiceMock.Object);

            var bob = new UserComponent { Name = "Bob", Email = "bob@super-company.com" };

            sendMailUserVisitorSUT.SendEmail(bob, email);

            mailerServiceMock.Verify(mailerService => mailerService.SendEmail(It.IsAny<string>(), email), Times.Once);
        }

        [Fact]
        public void SendEmail_UnknownType_ThrowsNotSupportedException()
        {
            var email = new Email("test", "test body");
            var mailerServiceMock = new Mock<IMailerService>();
            SendMailUserVisitor sendMailUserVisitorSUT = new SendMailUserVisitor(mailerServiceMock.Object);
            var userMock = new Mock<IUserComponent>();

            Assert.Throws<NotSupportedException>(() => sendMailUserVisitorSUT.SendEmail(userMock.Object, email));
        }


    }
}