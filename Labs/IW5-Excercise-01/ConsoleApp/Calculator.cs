using System;

namespace ConsoleApp
{
    internal class Calculator
    {
        /// <summary>
        /// Calculates basic mathematical operations (+,-,*,/) on given operands using 
        /// mathematical assembly implemented by students them selves.
        /// Writes results on console.
        /// </summary>
        internal static int Calculate(int operand1, int operand2, Program.MathOperation mathOperation)
        {
            switch (mathOperation)
            {
                case Program.MathOperation.Addition:
                    return Add(operand1, operand2);
                case Program.MathOperation.Subtraction:
                    return Substract(operand1, operand2);
                case Program.MathOperation.Multiplication:
                    return Multiply(operand1, operand2);
                case Program.MathOperation.Division:
                    return Divide(operand1, operand2);
                default:
                    throw new ArgumentOutOfRangeException(nameof(mathOperation), mathOperation, null);
            }
        }

        private static int Divide(int operand1, int operand2)
        {
            return operand1 / operand2;
        }

        private static int Multiply(int operand1, int operand2)
        {
            return operand1 * operand2;
        }

        private static int Substract(int operand1, int operand2)
        {
            return operand1 - operand2;
        }

        private static int Add(int operand1, int operand2)
        {
            return operand1 + operand2;
        }
    }
}