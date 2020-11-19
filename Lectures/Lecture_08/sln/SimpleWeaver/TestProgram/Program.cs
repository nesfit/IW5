using System;

namespace TestProgram
{
    class Program
    {
        static void Method1()
        {
            Console.WriteLine("Inside method 1");
        }

        static void Method2()
        {
            Console.WriteLine("Inside method 2");
        }

        static void Method3()
        {
            static void InnerMethod31()
            {
                Console.WriteLine("Inside inner-method 3.1");
            }

            static void InnerMethod32()
            {
                Console.WriteLine("Inside inner-method 3.2");
            }

            InnerMethod31();
            InnerMethod32();
        }

        static void Main(string[] args)
        {
            Method1();
            Method2();
            Method3();
        }
    }
}
