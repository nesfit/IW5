using System;
using NUnit.Framework;

namespace FirstProgram
{
    [TestFixture]
    internal class MethodArguments
    {
        private void Foo(ref int p)
        {
            p = p + 1; // Increment p by 1
        }

        private void Split(string name, out string firstNames, out string lastName)
        {
            var i = name.LastIndexOf(' ');
            firstNames = name.Substring(0, i);
            lastName = name.Substring(i + 1);
        }

        private static int Sum(params int[] ints)
        {
            var sum = 0;
            for (var i = 0; i < ints.Length; i++)
                sum += ints[i]; // Increase sum by ints[i]
            return sum;
        }


        private void NamedArgumentsFoo(int x = 2, int y = 3)
        {
            Console.WriteLine(x + y);
        }

        [Test]
        public void NamedArguments()
        {
            var a = 0;
            NamedArgumentsFoo(y: 4, x: 4);
            NamedArgumentsFoo(y: ++a, x: --a);
        }

        [Test]
        public void OutModifier()
        {
            string a, b;
            Split("Stevie Ray Vaughan", out a, out b);
            Console.WriteLine(a); // Stevie Ray
            Console.WriteLine(b); // Vaughan
        }

        [Test]
        public void ParamsModifier()
        {
            var total = Sum(1, 2, 3, 4);
            Console.WriteLine(total); // 10
        }

        [Test]
        public void RefModifier()
        {
            var x = 8;
            Foo(ref x); // Ask Foo to deal directly with x
            Console.WriteLine(x); // x is now 9
        }
    }
}