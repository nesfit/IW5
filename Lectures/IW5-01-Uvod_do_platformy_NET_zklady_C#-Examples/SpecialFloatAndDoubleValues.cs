using System;
using NUnit.Framework;

namespace FirstProgram
{
    [TestFixture]
    public class SpecialFloatAndDoubleValues
    {
        [Test]
        public void Example()
        {
            Console.WriteLine(double.NegativeInfinity); // -Infinity

            Console.WriteLine(1.0 / 0.0); // Infinity
            Console.WriteLine(-1.0 / 0.0); // -Infinity

            Console.WriteLine(0.0 / 0.0); // NaN

            Console.WriteLine(0.0 / 0.0 == double.NaN); // False
            Console.WriteLine(double.IsNaN(0.0 / 0.0)); // True

            Console.WriteLine(Equals(0.0 / 0.0, double.NaN));
        }

        [Test]
        public void RealNumberRoundingErrorsExample()
        {
            var tenth = 0.1f; // Not quite 0.1
            var one = 1f;
            Console.WriteLine(one - tenth * 10f); // −1.490116E-08

            var m = 1M / 6M; // 0.1666666666666666666666666667M
            var d = 1.0 / 6.0; // 0.16666666666666666

            var notQuiteWholeM = m + m + m + m + m + m; // 1.0000000000000000000000000002M
            var notQuiteWholeD = d + d + d + d + d + d; // 0.99999999999999989

            Console.WriteLine(notQuiteWholeM == 1M); // False
            Console.WriteLine(notQuiteWholeD < 1.0); // True
        }
    }
}