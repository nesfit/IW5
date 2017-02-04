using System.Security.Cryptography;
using NestedUsingDirectives.N1;

namespace Outer.Middle.Inner
{
    internal class Class1
    {
    }

    internal class Class2
    {
    }
}

namespace Outer
{
    namespace Middle
    {
        namespace Inner
        {
            internal class ClassA
            {
            }

            internal class ClassB
            {
            }
        }
    }
}

namespace X
{
    internal class Test
    {
        public void Method()
        {
            var rsa = RSA.Create(); // Don't need fully qualified name
        }
    }
}

namespace NameScoping
{
    namespace Outer
    {
        namespace Middle
        {
            internal class Class1
            {
            }

            namespace Inner
            {
                internal class Class2 : Class1
                {
                }
            }
        }
    }
}

namespace NameHiding
{
    namespace Outer
    {
        internal class Foo
        {
        }

        namespace Inner
        {
            internal class Foo
            {
            }

            internal class Test
            {
                private Foo f1; // = Outer.Inner.Foo
                private Outer.Foo f2; // = Outer.Foo
            }
        }
    }
}

namespace RepeatedNamespaces
{
    namespace Outer.Middle.Inner
    {
        internal class Class1
        {
        }
    }

    namespace Outer.Middle.Inner
    {
        internal class Class2
        {
        }
    }
}

namespace NestedUsingDirectives
{
    namespace N1
    {
        internal class Class1
        {
        }
    }

    namespace N2
    {
        internal class Class2 : Class1
        {
        }
    }

    namespace N2
    {
        // class Class3 : Class1 { } // Compile-time error
    }
}