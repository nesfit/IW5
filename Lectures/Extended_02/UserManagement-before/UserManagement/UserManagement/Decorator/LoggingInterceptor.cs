using System;
using Castle.DynamicProxy;

namespace UserManagement.Decorator
{
    public class LoggingInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var methodName = invocation.Method.Name;
            ConsoleLogMessage(ConsoleColor.Yellow, $"Method: {methodName} start");
            try
            {
                invocation.Proceed();
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