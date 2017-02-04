using System;

namespace IW5_OOP.Pets
{
    public sealed class Cat : Pet, IDrawableNickname
    {
        private readonly int _livesLeft = 9;
        private string _name;
        private int _age;

        public Cat(string name)
        {
            this.Name = name;
        }
        public Cat(string name, int livesLeft):this(name)
        {
            this._livesLeft = livesLeft;
        }

        public new string Name
        {
            get
            {
                return _name ?? (_name = $"{base.NickName} Cat");
            }
            private set {
                _name = null;
                base.NickName = value;
            }
        }

        public int Prise => _livesLeft*10;

        public string PriseUSD => $"{Prise} USD";

        public int Age
        {
            get { return _age; }
            set { _age = value > 0 ? value: _age - value; }
        }

        public override void Draw()

        {
            base.Draw();
            Console.WriteLine($"The cats`s name is {base.NickName} or {Name}?");
            Console.WriteLine($"The cat has only {_livesLeft} lifes left.");
            Console.WriteLine($"The cat`s prise is {PriseUSD}.");
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Age = {Age}";
        }
    }
}