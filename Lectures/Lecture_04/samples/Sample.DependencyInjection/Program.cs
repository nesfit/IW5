using System;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.DependencyInjection
{
    public class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length != 2 || !int.TryParse(args[1], out var count)) {
                Console.WriteLine("Usage: rng <console|file> <COUNT>");
                return 1;
            }

            var collection = new ServiceCollection();
            switch(args[0])
            {
                case "console":
                    collection.AddSingleton<ILogger, ConsoleLogger>();
                    break;
                case "file":
                    collection.AddSingleton<ILogger>(new FileLogger("rng.log"));
                    break;
                default:
                    Console.WriteLine($"'{args[0]}' is not a valid logger type");
                    return 1;
            }
            collection.AddSingleton<Rng>();

            var provider = collection.BuildServiceProvider();

            var rng = provider.GetRequiredService<Rng>();
            rng.Generate(count);

            return 0;
        }
    }
}
