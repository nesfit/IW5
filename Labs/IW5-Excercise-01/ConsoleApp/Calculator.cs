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
                    return operand1 + operand2;
                case Program.MathOperation.Subtraction:
                    return operand1 - operand2;
                case Program.MathOperation.Multiplication:
                    return operand1 * operand2;
                case Program.MathOperation.Division:
                    return operand1 / operand2;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mathOperation), mathOperation, null);
            }
        }
    }
}