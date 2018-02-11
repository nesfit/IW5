using System;
using OOP.Animals;
using Xunit;

namespace OOP
{
    public class EnumSample
    {
        private enum HorseColor
        {
            Siml = 0,
            Palomino = 5,
            Ryzak = 10
        }

        [Flags]
        public enum HorseType
        {
            None = 0,
            Racing = 1,
            Breeding = 2,
            ForSosages = 4,
            Dead = 8
        }

        [Fact]
        public void EnumHorseColorToIntTest()
        {
            HorseColor color = HorseColor.Siml;
            int colorNumber = (int)HorseColor.Ryzak;
            Assert.Equal(10, colorNumber);
        }

        [Fact]
        public void EnumHorseColorParseTest()
        {
            HorseColor.TryParse("Ryzak", out HorseColor color);
            Assert.Equal(HorseColor.Ryzak, color);
        }

        [Fact]
        public void EnumHorseTypeTest()
        {
            HorseType type = HorseType.Racing | HorseType.Breeding;
            type |= HorseType.ForSosages;
            Assert.True(type.HasFlag(HorseType.Racing));
            Assert.True(type.HasFlag(HorseType.Breeding));
            Assert.True(type.HasFlag(HorseType.ForSosages));
            Assert.False(type.HasFlag(HorseType.Dead));
            Console.WriteLine(type); //Racing, Breeding, ForSosages
        }
    }
}