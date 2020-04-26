using System;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace Sample.Sqrt
{
    class Program
    {
        static int Main(string[] args)
        {
            var rootCommand = new RootCommand("Square Root")
            {
                new Argument<double>("value")
                {
                    Description = "value to be square rooted"
                },
                new Option<bool>(new [] {"--color", "-c"}, "enables colored output")
            };
            rootCommand.Handler = CommandHandler.Create<double, bool>((value, color) =>
            {
                if (color)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                Console.WriteLine(Math.Sqrt(value));
            });
            return rootCommand.Invoke(args);
        }
    }
}
