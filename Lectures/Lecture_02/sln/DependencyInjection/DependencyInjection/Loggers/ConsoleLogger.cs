using System;

namespace DependencyInjection.Loggers
{
    public class ConsoleLogger : ILogger
    {
        private int lineCounter = 1;

        public void Log(string message)
        {
            Console.WriteLine($"Line {lineCounter}: {message}");
            lineCounter++;
        }
    }
}