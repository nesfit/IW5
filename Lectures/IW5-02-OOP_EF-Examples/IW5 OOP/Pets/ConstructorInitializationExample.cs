using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IW5_OOP.Pets
{
    public class ConstructorInitializationExample
    {
        public class Pet
        {
            public Pet()//5
            { 
                this.Name = "Jane Doe";//6
            }
            public string Name { get; protected set; } = "John Doe"; //4
        }

        public class Cat : Pet
        {
            private readonly int _livesLeft = 9; //1
            private int _age;

            public Cat(string name) : base()//3
            { 
                this.Name = name;//7
            }
            public Cat(string name, int livesLeft) : this(name)//2
            { 
                this._livesLeft = livesLeft;//8
            }

            public int Age
            {
                get { return _age; }
                set { _age = value; } //9
            }
        }
    }
}
