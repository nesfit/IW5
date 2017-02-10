using System;

namespace IW5_OOP
{
    public class PartialClass
    {
        public PartialClass()
        {
           var dog1Form = new Dog1Form();
        }

        // Dog1Gen.cs - auto-generated
        public partial class Dog1Form
        {
            public Dog1Form()
            {
                Bark();
            }

            partial void Bark();
        }

        // Dog1Form.cs - hand-authored
        public partial class Dog1Form
        {
            partial void Bark()
            {
                Console.WriteLine("Bark!!!");
            }
        }
    }
}