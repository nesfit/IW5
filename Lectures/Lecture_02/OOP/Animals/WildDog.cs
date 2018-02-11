using System;

namespace OOP.Animals
{
    public class WildDog : Animal
    {
        private string _lastBitten;

        public void Bite(string who)
        {
            this._lastBitten = who;
            Console.WriteLine($"{this.Name} bit {who}!");
        }
        public override void DrawIt() { Console.WriteLine($"{this.Name}"); }
    }
}