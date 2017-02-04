using NUnit.Framework;

namespace FirstProgram
{
    [TestFixture]
    internal class NumericConversions
    {
        [Test]
        public void FlotToIntegral()
        {
            var i = 1;
            float f = i;
            var i2 = (int) f;
        }

        [Test]
        public void IntgralToIntegral()
        {
            var x = 12345; // int is a 32-bit integral
            long y = x; // Implicit conversion to 64-bit integral
            var z = (short) x; // Explicit conversion to 16-bit integral
        }

        [Test]
        public void PrecisionLost()
        {
            var i1 = 100000001;
            float f = i1; // Magnitude preserved, precision lost
            var i2 = (int) f; // 100000000
        }
    }
}