using System;

namespace CleanCodeSample.MathExcercise
{
    class MathExerciseRunner
    {
        private readonly INumberReader numberReader;
        private readonly ITextWriter textWriter;

        public MathExerciseRunner(INumberReader numberReader, ITextWriter textWriter)
        {
            this.numberReader = numberReader;
            this.textWriter = textWriter;
        }

        public void Test(IMathOperation mathExcercise)
        {
            var leftOperand = numberReader.Read($"Zadejte 1. číslo pro {mathExcercise.OperationName}: ");
            var rightOperand = numberReader.Read($"Zadejte 2. číslo pro {mathExcercise.OperationName}: ");

            var askForSolution = $"{leftOperand} {mathExcercise.Operand} {rightOperand} = ";
            var userAnswer = numberReader.Read(askForSolution);

            var correctResult = mathExcercise.GetCorrectResult(leftOperand, rightOperand);
            WriteResultMessage(correctResult, userAnswer);
        }

        private void WriteResultMessage(int correctResult, int userAnswer)
        {
            var wasUserAnswerCorrect = correctResult == userAnswer;
            var wasAnswerCorrectMessage = GetWasAnswerCorrectMessage(wasUserAnswerCorrect);
            var messageType = GetMessageType(wasUserAnswerCorrect);

            string message = $"Vaše odpověď \"{userAnswer}\" {wasAnswerCorrectMessage}";
            textWriter.Write(message, messageType);
        }

        private static MessageType GetMessageType(bool wasUserAnswerCorrect)
        {
            return wasUserAnswerCorrect ? MessageType.Success : MessageType.Fail;
        }

        private static string GetWasAnswerCorrectMessage(bool wasUserAnswerCorrect)
        {
            return wasUserAnswerCorrect ? "byla správná" : "nebyla správná";
        }
    }
}