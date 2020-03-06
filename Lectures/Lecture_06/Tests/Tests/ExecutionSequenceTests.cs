using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class ExecutionSequenceTests
    {
        public TestContext TestContext { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            testContext.WriteLine("1. Executing ClassInitialize method.");
        }

        [TestInitialize]
        public void TestInitialize()
        {
            TestContext.WriteLine("2. Executing TestInitialize method.");
        }

        [TestMethod]
        public void TestMethod1()
        {
            TestContext.WriteLine("3a. Executing Test1 method.");
        }

        [TestMethod]
        public void TestMethod2()
        {
            TestContext.WriteLine("3b. Executing Test2 method.");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            TestContext.WriteLine("4. Executing TestCleanup method.");

        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Console.WriteLine("5. Executing ClassCleanup method.");
        }
    }
}
