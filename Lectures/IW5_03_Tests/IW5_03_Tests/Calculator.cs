using System;

namespace IW5_03_Tests
{
    internal static class Calculator
    {
        internal static int Calculate(Operations command, int first, int second)
        {
            switch (command)
            {
                case Operations.Add:
                    return Add(first, second);
                case Operations.Sub:
                    return Sub(first, second);
                default:
                    throw new InvalidOperationException("The command is not supported.");
            }
        }

        private static int Sub(int first, int second)
        {
            return first - second;
        }

        private static int Add(int first, int second)
        {
            return first + second;
        }
    }
}
