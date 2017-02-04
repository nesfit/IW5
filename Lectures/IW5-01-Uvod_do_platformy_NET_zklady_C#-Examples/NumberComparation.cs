using System;
using NUnit.Framework;

namespace FirstProgram
{
    [TestFixture]
    public class NumberComparation
    {
        private bool AlmostEqual(double a, double b)
        {
            const double tolerance = 0.00000001;

            if (a == b)
                return true;

            return Math.Abs(a - b) < tolerance;
        }

        [Test]
        public void Comparation()
        {
            var x = 49.0;

            var y = 1 / x;

            var calculatedResult = x * y;
            var expectedResult = 1.0;
            var areSame = calculatedResult == expectedResult;

            Console.WriteLine(areSame);
            Console.WriteLine(calculatedResult.Equals(expectedResult));
            Console.WriteLine(AlmostEqual(calculatedResult, expectedResult));
        }
    }
}