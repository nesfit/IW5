using System.Collections.Generic;
using CleanCodeSample.MathExcercise;

namespace CleanCodeSample
{
    class MathExcercisesProgram
    {
        private readonly List<IMathExcercise> mathExcercises = new List<IMathExcercise>
        {
            new AdditionMathExcercise(),
            new SubstractionMathExcercise(),
            new MultiplicationMathExcercise()
        };

        public void Run()
        {
            var mathExcerciseRunner = new MathExcerciseRunner();
            foreach (var mathExcercise in mathExcercises)
            {
                mathExcerciseRunner.Test(mathExcercise);
            }
        }
    }
}