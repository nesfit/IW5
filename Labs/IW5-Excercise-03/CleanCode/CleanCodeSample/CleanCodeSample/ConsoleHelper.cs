using System;

namespace CleanCodeSample
{
    public class ConsoleHelper
    {

        public int ReadNumber(string message)
        {
            Console.Write(message);
            int input;
            if (!int.TryParse(Console.ReadLine(), out input))
            {
                return ReadNumber(message);
            }
            return input;
        }

        public void WriteColorLine(ConsoleColor foregroundColor, string message)
        {
            var previouslyForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(message);
            Console.ForegroundColor = previouslyForegroundColor;
        }
    }
}