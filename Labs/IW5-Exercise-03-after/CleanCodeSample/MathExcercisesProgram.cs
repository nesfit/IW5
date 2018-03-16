using System.Collections.Generic;
using CleanCodeSample.MathExcercise;

namespace CleanCodeSample
{
    class MathExcercisesProgram
    {
        private readonly List<IMathOperation> mathExcercises = new List<IMathOperation>
        {
            new AdditionMathOperation(),
            new SubstractionMathOperation(),
            new MultiplicationMathOperation()
        };

        public void Run()
        {
            var consoleHelper = new ConsoleHelper();
            var mathExcerciseRunner = new MathExerciseRunner(consoleHelper, consoleHelper);
            foreach (var mathExcercise in mathExcercises)
            {
                mathExcerciseRunner.Test(mathExcercise);
            }
        }
    }
}