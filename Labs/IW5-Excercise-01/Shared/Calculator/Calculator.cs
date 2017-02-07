using System;

namespace Calculator
{
    public class Calculator
    {
        /// <summary>
        /// Calculates basic mathematical operations (+,-,*,/)
        /// </summary>
        public static int Calculate(int operand1, int operand2, MathOperation mathOperation)
        {
            switch (mathOperation)
            {
                case MathOperation.Addition:
                    return Add(operand1, operand2);
                case MathOperation.Subtraction:
                    return Substract(operand1, operand2);
                case MathOperation.Multiplication:
                    return Multiply(operand1, operand2);
                case MathOperation.Division:
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