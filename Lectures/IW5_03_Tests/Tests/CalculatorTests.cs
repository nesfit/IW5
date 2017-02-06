using System;
using IW5_03_Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void TwoFive_Add_Returns7()
        {
            int result = Calculator.Calculate(Operations.Add, 2, 5);
            Assert.AreEqual(7, result, " All operations need to follow mathematic rules.");
        }

        [TestMethod]
        public void TwoMaximum_Add_Returns7()
        {
            int result = Calculator.Calculate(Operations.Add, 2, int.MaxValue);
            Assert.AreEqual(-2147483647, result, " All operations need to follow mathematic rules.");
        }

        [TestMethod]
        public void FiveOne_Add_Returns4()
        {
            int result = Calculator.Calculate(Operations.Sub, 5, 1);
            Assert.AreEqual(4, result, " All operations need to follow mathematic rules.");
        }
    }
}
