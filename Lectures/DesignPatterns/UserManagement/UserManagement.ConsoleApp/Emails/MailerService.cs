using System;

namespace UserManagement.ConsoleApp.Emails
{
    public class MailerService : IMailerService
    {
        public void SendEmail(string to, Email email)
        {
            Console.WriteLine($"Send email to: {to}");
            Console.WriteLine($"\t{email.Subject} - {email.Body}");
        }
    }
}