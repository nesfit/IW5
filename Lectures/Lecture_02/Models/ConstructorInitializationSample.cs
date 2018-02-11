using OOP.Animals;
using Xunit;

namespace OOP
{
    public class ConstructorInitializationSample
    {
        [Fact]
        public void ConstructorInitializationTest()
        {
            new Cat("Betty", 7);
        }
    }
}
