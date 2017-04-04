using System;
using UserManagement.ConsoleApp.Emails;

namespace UserManagement.ConsoleApp.Interceptor
{
    public class LoggedEmailService : IMailerService
    {
        private readonly IMailerService mailerServiceImplementation;

        public LoggedEmailService(IMailerService mailerServiceImplementation)
        {
            this.mailerServiceImplementation = mailerServiceImplementation;
        }

        public void SendEmail(string to, Email email)
        {
            var methodName = "SendEmail";
            PrintColorText(ConsoleColor.Yellow, $"Method: {methodName} start");
            try
            {
                mailerServiceImplementation.SendEmail(to, email);
                PrintColorText(ConsoleColor.Yellow, $"Method: {methodName} was succeed");
            }
            catch (Exception e)
            {
                PrintColorText(ConsoleColor.Red, $"Exception {e} was raised in Method: {methodName}");
                throw;
            }
            PrintColorText(ConsoleColor.Yellow, $"Method: {methodName} exit");
        }
        
        private void PrintColorText(ConsoleColor color, string message, params object[] args)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message, args);
            Console.ForegroundColor = originalColor;
        }

    }
}