using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCodeSample
{
    class Program
    {
        static void Main(string[] args)
        {
            TestAddition();

            // Waiting for input from user
            Console.ReadKey();
        }
        
        private static void TestAddition()
        {
            var firstInputForAddition = ReadNumberInput("Zadejte 1. číslo pro sčítání: ");
            var secondInputForAddition = ReadNumberInput("Zadejte 2. číslo pro sčítání: ");
            var userAnswer = ReadNumberInput("Zadejte Vaši odpověď: ");

            var result = firstInputForAddition + secondInputForAddition;
            
            WriteResultMessage(result, userAnswer);
        }

        private static void WriteResultMessage(int result, int userAnswer)
        {
            string message = $"Vaše odpověď \"{userAnswer}\"";

            if (result == userAnswer)
            {
                WriteColorLine(ConsoleColor.Green, $"{message} byla správná");
            }
            else
            {
                WriteColorLine(ConsoleColor.Red, $"{message} nebyla správná");
            }
        }

        private static int ReadNumberInput(string message)
        {
            Console.Write(message);
            return ReadNumberInput();
        }

        private static int ReadNumberInput()
        {
            int input;
            // Reading number from console
            while (!int.TryParse(Console.ReadLine(), out input))
            {
            }
            return input;
        }

        private static void WriteColorLine(ConsoleColor foregroundColor, string message)
        {
            var previouslyForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(message);
            Console.ForegroundColor = previouslyForegroundColor;
        }
    }
}
