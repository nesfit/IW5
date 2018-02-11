using System;
using OOP.Animals;
using Xunit;

namespace OOP
{
    public class CastSample
    {
        [Fact]
        public void UpCastTest()
        {
            var dog = new Dog("Alik");
            WildDog wildDog = dog;
            Assert.Equal(wildDog, dog);
        }

        [Fact]
        public void DownCastTest()
        {
            WildDog wildDog = new Dog("Alik");
            var dog = (Dog) wildDog;
            Assert.Equal(wildDog, dog);
        }

        [Fact]
        public void DownCastFailTest()
        {
            object wildDog = new Dog("Alik");
            Assert.Throws<InvalidCastException>(()=>(Cat) wildDog);
        }

        [Fact]
        public void CastTest()
        {
            var dog = new Dog("Alik");
            WildDog wildDog = dog;

            Dog dog1 = (Dog)wildDog;

            Assert.Equal(dog, dog1);
            Assert.Equal(wildDog, dog);
            Assert.Equal(wildDog, dog1);
        }

        [Fact]
        public void AsOperatorTest()
        {
            WildDog wildDog = new WildDog();
            Assert.Null(wildDog as Dog);
        }

        [Fact]
        public void IsOperatorTest()
        {
            WildDog wildDog = new WildDog();
            Assert.False(wildDog is Dog);
        }

        [Fact]
        public void WithoutPatternMatchingTest()
        {
            WildDog wildDog = new WildDog();
            if (wildDog is Dog)
            {
                Assert.NotNull(wildDog as Dog);
            }
            Assert.Null(wildDog as Dog);
        }

        [Fact]
        public void PatternMatchingTest()
        {
            WildDog wildDog = new WildDog();
            if (wildDog is Dog dog)
            {
                Assert.NotNull(dog);
            }
        }
    }
}