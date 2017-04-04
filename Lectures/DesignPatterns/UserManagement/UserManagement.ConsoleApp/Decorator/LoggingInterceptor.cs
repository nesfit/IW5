using Castle.DynamicProxy;
using System;

namespace UserManagement.ConsoleApp.Interceptor
{
    public class LoggingInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var methodName = invocation.Method.Name;
            PrintColorText(ConsoleColor.Yellow, $"Method: {methodName} start");
            try
            {
                invocation.Proceed();
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