using System;
using Calculator;
using CommandLine;
using CommandLine.Text;

namespace CalculatorConsole
{
    /// <summary>
    ///     CommandLine parser settings
    ///     https://commandline.codeplex.com/
    /// </summary>
    internal class CommandLineOptions
    {
        [Option('f', "first", Required = false, HelpText = "The first operand.")]
        public int? First { get; set; }

        [Option('s', "second", Required = false, HelpText = "The second operand.")]
        public int? Second { get; set; }

        [Option('o', "operation", Required = true, HelpText = "The operation.")]
        public MathOperation? Operation { get; set; }

        [Option('v', "verbose", DefaultValue = false, HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        [HelpOption]
        public static string GetUsage()
        {
            var help = new HelpText
            {
                Heading = new HeadingInfo("<<app title>>", "<<app version>>"),
                Copyright = new CopyrightInfo("<<app author>>", DateTime.Now.Year),
                AdditionalNewLineAfterOption = true,
                AddDashesToOption = true
            };
            help.AddPreOptionsLine("<<license details here.>>");
            help.AddPreOptionsLine("Usage: app -f 4 -s 3");
            help.AddOptions(new CommandLineOptions());
            return help;
        }
    }
}