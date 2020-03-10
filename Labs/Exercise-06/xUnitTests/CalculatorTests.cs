using System;
using IW5_Tests;
using Xunit;

namespace xUnitTests
{
    public class CalculatorTests
    {
        [Theory]
        [InlineData(1, 2)]
        [InlineData(0, 0)]
        [InlineData(-1, -2)]
        [InlineData(1, int.MaxValue - 1)]
        [InlineData(-1, int.MinValue + 1)]
        public void AddTest(int number1, int number2)
        {
            int expectedResult = number1 + number2;

            var actualResult = Calculator.Calculate(Operations.Add, number1, number2);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void AddOverflowTest()
        {
            Assert.Throws<OverflowException>(() => Calculator.Calculate(Operations.Add, int.MaxValue, 1));
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(0, 0)]
        [InlineData(-1, -2)]
        [InlineData(1, int.MaxValue)]
        [InlineData(-1, int.MinValue)]
        public void SubtractTest(int number1, int number2)
        {
            int expectedResult = number1 - number2;

            var actualResult = Calculator.Calculate(Operations.Sub, number1, number2);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void SubOverflowTest()
        {
            Assert.Throws<OverflowException>(() => Calculator.Calculate(Operations.Sub, int.MinValue, 1));
        }

        [Fact]
        public void UnknownCommandTest()
        {
            Assert.Throws<InvalidOperationException>(() => Calculator.Calculate((Operations)int.MinValue, 1, 1));
        }

    }
}