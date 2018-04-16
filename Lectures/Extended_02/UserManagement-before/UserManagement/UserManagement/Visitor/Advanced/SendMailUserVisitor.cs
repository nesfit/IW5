using UserManagement.Composite;
using UserManagement.Models;
using UserManagement.Services;

namespace UserManagement.Visitor.Advanced
{
    public class SendMailUserVisitor : UserVisitorBase
    {

        private readonly IEmailService emailService;
        private Email email;

        public SendMailUserVisitor(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        public void SendEmail(IContactComponent contactComponent, Email email)
        {
            this.email = email;
            base.Visit(contactComponent);
        }
        
        protected override void Visit(ContactComponent contactComponent)
        {
            emailService.SendEmail(contactComponent.EmailAddress, email);
        }

        protected override void Visit(ContactGroupComposite contactGroupComposite)
        {
            foreach (var member in contactGroupComposite.Members)
            {
                SendEmail(member, email);
            }
        }
    }
}