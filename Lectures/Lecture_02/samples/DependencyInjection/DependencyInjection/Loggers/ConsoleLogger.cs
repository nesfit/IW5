using System;

namespace DependencyInjection.Loggers
{
    public class ConsoleLogger : ILogger
    {
        private static int LoggerCount = 0;
        private int loggerId = 0;
        private int lineCounter = 1;

        public ConsoleLogger()
        {
            LoggerCount++;
            loggerId = LoggerCount;
        }

        public void Log(string message)
        {
            Console.WriteLine($"Logger: {loggerId} Line {lineCounter}: {message}");
            lineCounter++;
        }
    }
}