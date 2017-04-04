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
        public void SendEmail_DesignersGroup_SendsEmailToAllInGroup()
        {
            var email = new Email("test", "test body");
            var mailerServiceMock = new Mock<IMailerService>();
            mailerServiceMock.Setup(mailerService => mailerService.SendEmail(It.IsAny<string>(), email));
            SendMailUserVisitor sendMailUserVisitorSUT = new SendMailUserVisitor(mailerServiceMock.Object);

            sendMailUserVisitorSUT.SendEmail(EmployeeStorage.Instance.Designers, email);

            mailerServiceMock.Verify(mailerService => mailerService.SendEmail(It.IsAny<string>(), email), Times.Exactly(2));
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