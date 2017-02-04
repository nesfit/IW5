using System;
using NUnit.Framework;

namespace FirstProgram
{
    [TestFixture]
    public class IntegralOverflow
    {
        [Test]
        public void CheckedOverflowExample()
        {
            var a = int.MinValue;
            Assert.Throws<OverflowException>(() => { a = checked(a--); // throw OverflowException
            });

            Console.WriteLine(a == int.MaxValue);
        }

        [Test]
        public void OverflowExample()
        {
            var a = int.MinValue;
            a--;
            Console.WriteLine(a == int.MaxValue); // True
        }
    }
}