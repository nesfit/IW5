using System;

namespace IW5_OOP.Pets
{
    public sealed class Cat : Pet, IDrawableNickname
    {
        private readonly int _livesLeft = 9;
        private int _age;
        private string _name;

        public Cat(string name)
        {
            Name = name;
        }

        public Cat(string name, int livesLeft) : this(name)
        {
            _livesLeft = livesLeft;
        }

        public string Name
        {
            get { return _name ?? (_name = $"{NickName} Cat"); }
            private set
            {
                _name = null;
                NickName = value;
            }
        }

        public int Prise => _livesLeft * 10;

        public string PriseUSD => $"{Prise} USD";

        public int Age
        {
            get { return _age; }
            set { _age = value > 0 ? value : _age - value; }
        }

        public override void Draw()

        {
            base.Draw();
            Console.WriteLine($"The cats`s name is {NickName} or {Name}?");
            Console.WriteLine($"The cat has only {_livesLeft} lifes left.");
            Console.WriteLine($"The cat`s prise is {PriseUSD}.");
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Age = {Age}";
        }
    }
}