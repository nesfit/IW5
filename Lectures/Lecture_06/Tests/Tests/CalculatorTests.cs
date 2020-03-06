using System;
using IW5_Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void TwoAddFive_Calculate_Returns7()
        {
            int result = Calculator.Calculate(Operations.Add, 2, 5);
            Assert.AreEqual(7, result, " All operations need to follow mathematic rules.");
        }

        [TestMethod]
        public void TwoAddMaximum_Calculate_Returns7()
        {
            int result = Calculator.Calculate(Operations.Add, 2, int.MaxValue);
            Assert.AreEqual(-2147483647, result, " All operations need to follow mathematic rules.");
        }

        [TestMethod]
        public void FiveSubOne_Calculate_Returns4()
        {
            int result = Calculator.Calculate(Operations.Sub, 5, 1);
            Assert.AreEqual(4, result, " All operations need to follow mathematic rules.");
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void UnknownCommand_Calculate_Throws()
        {
            Calculator.Calculate((Operations)int.MaxValue, 5, 1);
        }
    }
}
