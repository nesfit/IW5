namespace IW5_OOP.Pets
{
    public class ConstructorInitializationExample
    {
        public class Pet
        {
            public Pet() //5
            {
                Name = "Jane Doe"; //6
            }

            public string Name { get; protected set; } = "John Doe"; //4
        }

        public class Cat : Pet
        {
            private readonly int _livesLeft = 9; //2

            public Cat(string name) //3
            {
                Name = name; //7
            }

            public Cat(string name, int livesLeft) : this(name) //1
            {
                _livesLeft = livesLeft; //8
            }

            public int Age { get; set;
//9
            }
        }
    }
}