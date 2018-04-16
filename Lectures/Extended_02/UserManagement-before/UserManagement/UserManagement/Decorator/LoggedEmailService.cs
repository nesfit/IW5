using System;
using UserManagement.Models;
using UserManagement.Services;

namespace UserManagement.Decorator
{
    public class LoggedEmailService : IEmailService
    {
        private readonly IEmailService emailServiceImplementation;

        public LoggedEmailService(IEmailService emailServiceImplementation)
        {
            this.emailServiceImplementation = emailServiceImplementation;
        }

        public void SendEmail(string to, Email email)
        {
            var methodName = "SendEmail";
            ConsoleLogMessage(ConsoleColor.Yellow, $"Method: {methodName} start");
            try
            {
                emailServiceImplementation.SendEmail(to, email);
                ConsoleLogMessage(ConsoleColor.Yellow, $"Method: {methodName} was succeed");
            }
            catch (Exception e)
            {
                ConsoleLogMessage(ConsoleColor.Red, $"Exception {e} was raised in Method: {methodName}");
                throw;
            }
            ConsoleLogMessage(ConsoleColor.Yellow, $"Method: {methodName} exit");
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