using System;

namespace IW5_OOP.Pets
{
    public abstract class Animal : IDraw
    {
        public abstract void Draw();
    }

    public class Pet : Animal, INickName
    {
        public string NickName { get; protected set; } = "John Doe";

        public override void Draw()
        {
            Console.WriteLine($"Drawing class {GetType().Name}");
        }

        public override string ToString()
        {
            return $"Name = {NickName}";
        }
    }
    
}