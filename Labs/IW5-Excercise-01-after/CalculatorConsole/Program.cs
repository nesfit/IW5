using System;

namespace CalculatorConsole
{
    internal partial class Program
    {
        internal static void LogExcetion(Exception exception)
        {
            Console.WriteLine($"Exception occured: {exception.Message}");
        }

        internal static void LogMessage(string message)
        {
            Console.WriteLine(message);
        }

        internal static void Main(string[] args)
        {
            try
            {
                var options = CommandLineParser.ParseCommandLineCommands(args);
                CalculatorWrapper.Calculate(options);
            }
            catch (IncorrentCommandLineArguments ex)
            {
                LogExcetion(ex);
                LogMessage(CommandLineOptions.GetUsage());
            }
            catch (ArgumentNullException ex)
            {
                LogExcetion(ex);
            }

            WaitBeforeExit();
        }

        private static void WaitBeforeExit()
        {
            Console.WriteLine("Press ANY key to continue...");
            Console.ReadKey();
        }
    }
}