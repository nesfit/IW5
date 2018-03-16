using System;

namespace CleanCodeSample
{
    public class ConsoleHelper : INumberReader, ITextWriter
    {
        public int Read(string message)
        {
            Console.Write(message);
            int input;
            if (!int.TryParse(Console.ReadLine(), out input))
            {
                return Read(message);
            }
            return input;
        }

        public void Write(string message, MessageType type = MessageType.Normal)
        {
            var previouslyForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = GetMessageColor(type);
            Console.WriteLine(message);
            Console.ForegroundColor = previouslyForegroundColor;
        }

        private static ConsoleColor GetMessageColor(MessageType type)
        {
            switch (type)
            {
                case MessageType.Success:
                    return ConsoleColor.Green;
                case MessageType.Fail:
                    return ConsoleColor.Red;
                case MessageType.Normal:
                default:
                    return Console.ForegroundColor;
            }
        }
    }
}