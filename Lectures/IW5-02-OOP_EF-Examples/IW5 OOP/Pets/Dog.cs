using System;

namespace IW5_OOP.Pets
{
    public sealed class Dog : Pet
    {
        public Dog(string nickName) // constructor
        {
            NickName = nickName;
        }

        public override void Draw()
        {
            base.Draw();
            Console.WriteLine($"The Dog`s name is {NickName}.");
        }

        public void Bite(string who)
        {
            Console.WriteLine($"Bitting: {who}.");
        }
    }
}