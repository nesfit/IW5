using System;
using System.Text;

namespace Tests
{
    public class ValueParameter
    {
        static void Foo(StringBuilder fooSB)
        {
            fooSB.Append("test");
            fooSB = null;
        }

        public void Test()
        {
            StringBuilder sb = new StringBuilder();
            Foo(sb);
            Console.WriteLine(sb.ToString());      // test
        }
    }
}