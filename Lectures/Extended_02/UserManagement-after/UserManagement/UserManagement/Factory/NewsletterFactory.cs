using UserManagement.Models;
using UserManagement.Services;

namespace UserManagement.Factory
{
    public class NewsletterFactory
    {
        public NewsletterFactory(IEmailService emailService)
        {
            
        }

        public Email CreateNewsletter()
        {
            string subject = "Máme pro vás jedinečnou nabídku!";
            string body = "Začněte používat návrhové vzory právě teď!";

            return new Email(subject, body);
        }
    }
}