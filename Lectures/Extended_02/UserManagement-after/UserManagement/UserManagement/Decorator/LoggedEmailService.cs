using System;
using UserManagement.Models;
using UserManagement.Services;

namespace UserManagement.Decorator
{
    class LoggedEmailService : IEmailService
    {
        private IEmailService emailServiceImplementation;

        public LoggedEmailService(IEmailService emailServiceImplementation)
        {
            this.emailServiceImplementation = emailServiceImplementation;
        }

        public void SendEmail(string to, Email email)
        {
            try
            {
                ConsoleLogMessage(ConsoleColor.Yellow, $"Method {nameof(SendEmail)} start");
                emailServiceImplementation.SendEmail(to, email);
                ConsoleLogMessage(ConsoleColor.Yellow, $"Method {nameof(SendEmail)} was succeed");
            }
            catch (Exception e)
            {
                ConsoleLogMessage(ConsoleColor.Yellow, $"Method {nameof(SendEmail)} throw: {e}");
                throw;
            }
        }

        private void ConsoleLogMessage(ConsoleColor color, string message, params object[] args)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message, args);
            Console.ForegroundColor = originalColor;
        }
    }
}