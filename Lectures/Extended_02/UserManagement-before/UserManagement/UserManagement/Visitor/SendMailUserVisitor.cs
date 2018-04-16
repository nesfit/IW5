using System;
using UserManagement.Composite;
using UserManagement.Models;
using UserManagement.Services;

namespace UserManagement.ConsoleApp.Visitor
{
    public class SendMailUserVisitor
    {

        private readonly IEmailService emailService;

        public SendMailUserVisitor(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        public void SendEmail(IContactComponent contactComponent, Email email)
        {
            if (contactComponent is ContactComponent)
            {
                SendEmail((ContactComponent)contactComponent, email);
            }
            else if (contactComponent is ContactGroupComposite)
            {
                SendEmail((ContactGroupComposite)contactComponent, email);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private void SendEmail(ContactComponent contactComponent, Email email)
        {
            emailService.SendEmail(contactComponent.EmailAddress, email);
        }

        private void SendEmail(ContactGroupComposite contactGroupComposite, Email email)
        {
            foreach (var member in contactGroupComposite.Members)
            {
                SendEmail(member, email);
            }
        }
    }
}