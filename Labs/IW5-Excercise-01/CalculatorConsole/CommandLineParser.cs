namespace CalculatorConsole
{
    internal class CommandLineParser
    {
        /// <summary>
        /// Parses commandline agruments
        /// </summary>
        /// <param name="args">Command line arguments</param>
        /// <returns>Parsed arguments</returns>
        internal static CommandLineOptions ParseCommandLineCommands(string[] args)
        {
            var options = new CommandLineOptions();
            if (!CommandLine.Parser.Default.ParseArguments(args, options))
            {
                throw new Program.IncorrentCommandLineArguments("Bad arguments!", options);
            }
            return options;
        }
    }
}