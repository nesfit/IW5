using System;
using UserManagement.ConsoleApp.Composite;
using UserManagement.ConsoleApp.Emails;

namespace UserManagement.ConsoleApp.Visitor
{
    public class SendMailUserVisitor
    {

        private readonly IMailerService mailerService;

        public SendMailUserVisitor(IMailerService mailerService)
        {
            this.mailerService = mailerService;
        }

        public void SendEmail(IUserComponent userComponent, Email email)
        {
            if (userComponent is UserComponent)
            {
                SendEmail((UserComponent)userComponent, email);
            }
            else if (userComponent is UserGroupComposite)
            {
                SendEmail((UserGroupComposite)userComponent, email);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private void SendEmail(UserComponent userComponent, Email email)
        {
            mailerService.SendEmail(userComponent.Email, email);
        }

        private void SendEmail(UserGroupComposite userGroupComposite, Email email)
        {
            foreach (var member in userGroupComposite.Members)
            {
                SendEmail(member, email);
            }
        }
    }
}