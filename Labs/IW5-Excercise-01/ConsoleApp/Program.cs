// - Nastaveni projektu: output path, output type, target framework (kde a co je vystup)
// - Struktura aplikace: reference, Namespace, using, classes
// - try, catch
// - Debugging, F10,F11, vraceni provedene operace
//      - krokujte program, Okna: Watch, BreakPoints, conditional break point, call stack object browser
//      - vyskousejte System.Diagnostics.Debug.Write("");
// - vytvorte namespace IW5_cv01.Math v novem projektu typu knihovna, pridejte referenci
//      - v tomto projektu vytvorte funkce pro nasobeni, deleni, odcitani a stitani
// - vyuzitim tohoto suboru vytvorte jednoduchy kalkulator v konzoli
// - vyskousejte praci s parametry args, s kterymi byla aplikace spustena,
//      - (Project > Properties > Debug > Command line arguments)
// - Zkuste puzivat System.Math funkce
// - pozor na deleni nulou!

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal partial class Program
    {
        /// <summary>
        /// function Main - has to be static, has to have a name "Main", doesn't have to return a value
        ///               - cannot call non-static methods
        /// </summary>
        /// <param name="args">array of strings which represents parameters of the program</param>
        private static void Main(string[] args)
        {
            RunForLoop();

            RunWhileLoop();

            ShowIf(5);

            GetInput();

            Calculator();

            // stop the program at the end
            WaitForPressedKey();
        }
        
        /// <summary>
        /// Runs for loop, note: has to be static if we want to call it from Main
        /// </summary>
        private static void RunForLoop()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("For: {0}", i);
            }
        }

        /// <summary>
        /// Runs while loop, note: has to be static if we want to call it from Main
        /// </summary>
        private static void RunWhileLoop()
        {
            int index = 0;

            while (index < 11)
            {
                index = index + 1;
                Console.WriteLine($"Do - while: {index}");
            }
        }

        /// <summary>
        /// Shows if branching, note: has to be static if we want to call it from Main
        /// </summary>
        private static void ShowIf(int condition)
        {
            if (condition > 3)
            {
                Console.WriteLine("condition is greater than 3");
            }
            else
            {
                Console.WriteLine("condition is not greater than 3");
            }
        }

        /// <summary>
        /// Gets input from keyboard, note: has to be static if we want to call it from Main
        /// </summary>
        private static void GetInput()
        {
            try
            {
                Console.WriteLine("Waiting for input (number)...");
                int a = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input: {0}", a);
            }
            catch (FormatException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new ApplicationException("HUpsss....");
            }
            finally
            {
                // executes always, after the try and/or catch
                Console.WriteLine("Release resources... cleaning after my self");
            }
        }

        /// <summary>
        /// Helper - waits for any key to be pressed on a keyboard
        /// Holds the command prompt opened before program exits.
        /// </summary>
        private static void WaitForPressedKey()
        {
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        /// <summary>
        /// Calculates basic mathematical operations (+,-,*,/) on given operands using 
        /// mathematical assembly implemented by students them selves.
        /// Writes results on console.
        /// </summary>
        private static void Calculator(int operand1, int operand2, MathOperation mathOperation )
        {

        }
    }
}
