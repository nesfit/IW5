using System;
using NUnit.Framework;

namespace FirstProgram
{
    [TestFixture]
    internal class NumericLiterals
    {
        [Test]
        public void ShowLiterals()
        {
            // Notations
            var x = 127;
            var y = 0xff; // hexadecimal notation

            var d = 0.1;
            var d2 = .1; // valid without 0
            var milion = 1e06; // exponencial notation

            // Numeric types of constants
            Console.WriteLine(1.0.GetType()); // Double (double)
            Console.WriteLine(1E06.GetType()); // Double (double)
            Console.WriteLine(1.GetType()); // Int32 (int)
            Console.WriteLine(0xF0000000.GetType()); // UInt32 (uint)

            // Numeric suffixes
            Console.WriteLine(1f.GetType()); // Float (float)
            Console.WriteLine(1d.GetType()); // Double (doulbe)
            Console.WriteLine(1m.GetType()); // decimal (decimal)
            Console.WriteLine(1u.GetType()); // UInt32 (uint)
            Console.WriteLine(1L.GetType()); // Int64 (long)
            Console.WriteLine(1ul.GetType()); // UInt64 (ulong)      
        }
    }
}