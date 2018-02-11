using OOP.Animals;
using Xunit;

namespace OOP
{
    public class SystemObjectSample
    {
        [Fact]
        public void GetTypeTest()
        {
            var dog = new Dog("Alik");
            Assert.IsType<Dog>(dog);
            Assert.Equal("Dog", dog.GetType().Name);
        }

        [Fact]
        public void TypeOfTest()
        {
            Assert.Equal("Dog", typeof(Dog).Name);
        }

        [Fact]
        public void TypeOfGetTypeTest()
        {
            var dog = new Dog("Alik");
            Assert.Equal(typeof(Dog), dog.GetType());
        }
    }
}