using System;
using UserManagement.Composite;
using UserManagement.Models;
using UserManagement.Services;

namespace UserManagement.Visitor
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
            switch (contactComponent)
            {
                case ContactComponent contact:
                    SendEmail(contact, email);
                    break;
                case ContactGroupComposite contactGroup:
                    SendEmail(contactGroup, email);
                    break;
                default:
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