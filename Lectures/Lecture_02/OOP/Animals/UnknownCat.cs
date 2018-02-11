using System;

namespace OOP.Animals
{
    public class UnknownCat : Animal
    {
        protected int LivesLeft = 9;
        private readonly int _eyes = 2, _legs = 4;

        public override void DrawIt()
        {
            Console.WriteLine($"{this.GetType().Name} named: {this.Name}, with {this._legs} legs and {this._eyes} eyes.");
        }
    }
}