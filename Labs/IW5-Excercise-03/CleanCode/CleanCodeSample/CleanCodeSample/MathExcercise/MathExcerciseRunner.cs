using System;

namespace CleanCodeSample.MathExcercise
{
    class MathExcerciseRunner
    {
        protected ConsoleHelper consoleHelper = new ConsoleHelper();

        public void Test(IMathExcercise mathExcercise)
        {
            var leftOperand = consoleHelper.ReadNumber($"Zadejte 1. číslo pro {mathExcercise.OperationName}: ");
            var rightOperand = consoleHelper.ReadNumber($"Zadejte 2. číslo pro {mathExcercise.OperationName}: ");

            var askForSolution = $"{leftOperand} {mathExcercise.Operand} {rightOperand} = ";
            var userAnswer = consoleHelper.ReadNumber(askForSolution);

            var correctResult = mathExcercise.GetCorrectResult(leftOperand, rightOperand);
            WriteResultMessage(correctResult, userAnswer);
        }

        private void WriteResultMessage(int correctResult, int userAnswer)
        {
            var wasUserAnswerCorrect = correctResult == userAnswer;
            var wasAnswerCorrectMessage = GetWasAnswerCorrectMessage(wasUserAnswerCorrect);
            var messageColor = GetMessageColor(wasUserAnswerCorrect);

            string message = $"Vaše odpověď \"{userAnswer}\" {wasAnswerCorrectMessage}";
            consoleHelper.WriteColorLine(messageColor, message);
        }

        private static ConsoleColor GetMessageColor(bool wasUserAnswerCorrect)
        {
            return wasUserAnswerCorrect ? ConsoleColor.Green : ConsoleColor.Red;
        }

        private static string GetWasAnswerCorrectMessage(bool wasUserAnswerCorrect)
        {
            return wasUserAnswerCorrect ? "byla správná" : "nebyla správná";
        }
    }
}