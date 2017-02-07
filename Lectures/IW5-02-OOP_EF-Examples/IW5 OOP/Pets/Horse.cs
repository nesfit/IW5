using System;

namespace IW5_OOP.Pets
{
    public sealed class Horse : Animal
    {
        public enum HorseColor
        {
            Siml = 0,
            Palomino,
            Ryzak
        }

        [Flags]
        public enum HorseType
        {
            None = 0,
            Racing = 1,
            Breeding = 2,
            ForSosages = 4
        }

        private readonly int _numberOfLegs = 4;

        private HorseColor _color;

        public HorseColor Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public HorseType Type { get; set; }

        public override void Draw()
        {
            Console.WriteLine($"Horse - Count of horse`s legs is {_numberOfLegs}.");
        }

        public void SetColor(int color)
        {
            Color = (HorseColor) color;
        }

        public void SetColor(string color)
        {
            Enum.TryParse(color, out _color);
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Color = {Color}";
        }
    }
}