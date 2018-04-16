using System;
using UserManagement.Models;

namespace UserManagement.Services
{
    public class EmailService
    {
        public void SendEmail(string to, Email email)
        {
            Console.WriteLine($"Send email to: {to}");
            Console.WriteLine($"\t{email.Subject} - {email.Body}");
        }
    }
}