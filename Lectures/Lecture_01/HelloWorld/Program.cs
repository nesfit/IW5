using System;                                   // Importing namespace

namespace HelloWorld                            // Namespace declaration
{
    internal class Program                      // Class declaration
    {
        private static void Main(string[] args) // Method declaration
        {
            Console.WriteLine("Hello World!");  // Statement 1

            Console.WriteLine(                  // Statement 2
                "Press ANY key to continue...");
            Console.ReadKey();                  // Statement 3
        }                                       // End of method
    }
}
