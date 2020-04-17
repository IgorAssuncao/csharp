using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Calculator.Tests
{
    [TestClass()]
    public class CalculatorTest
    {
        public TestContext TestContext { get; set; }
        private static TestContext _testContext;

        public static Calculator calculator;

        // Run before all tests
        [ClassInitialize]
        public static void InitializeTestClass(TestContext testContext)
        {
            _testContext = testContext;
            calculator = new Calculator();
        }

        // Run after all tests
        [ClassCleanup]
        public static void CleanUpTestClass()
        {
            _testContext = null;
            calculator = null;
        }

        // Run before each test
        [TestInitialize]
        public void SetupTest()
        {
            calculator = new Calculator();
            // Console.WriteLine("TestContext.TestName='{0}'", _testContext.TestName);
        }
    }

    [TestClass()]
    public class CalculatorSumTest : CalculatorTest
    {
        [TestMethod()]
        public void SumTwoPositiveNumbersTest()
        {
            decimal expectedResult = 2;

            decimal methodResult = calculator.SumTwoNumbers(1, 1);

            Console.WriteLine(calculator);

            Assert.AreEqual(expectedResult, methodResult);
        }

        [TestMethod()]
        public void SumTwoNegativeNumbersTest()
        {
            decimal expectedResult = -2;

            decimal methodResult = calculator.SumTwoNumbers(-1, -1);

            Assert.AreEqual(expectedResult, methodResult);
        }

        [TestMethod()]
        public void SumTwoPositiveDecimalNumbersTest()
        {
            decimal expectedResult = 0.2m;

            decimal methodResult = calculator.SumTwoNumbers(0.1m, 0.1m);

            Assert.AreEqual(expectedResult, methodResult);
        }

        [TestMethod()]
        public void SumTwoNegativeDecimalNumbersTest()
        {
            decimal expectedResult = -0.2m;

            decimal methodResult = calculator.SumTwoNumbers(-0.1m, -0.1m);

            Assert.AreEqual(expectedResult, methodResult);
        }
    }

    [TestClass()]
    public class CalculatorSubtractTest : CalculatorTest
    {
        [TestMethod()]
        public void SubtractTwoPositiveNumbersTest()
        {
            decimal expectedResult = 0;

            decimal methodResult = calculator.SubtractTwoNumbers(1, 1);

            Assert.AreEqual(expectedResult, methodResult);
        }

        [TestMethod()]
        public void SubtractTwoNegativeNumbersTest()
        {
            decimal expectedResult = 0;

            decimal methodResult = calculator.SubtractTwoNumbers(-1, -1);

            Assert.AreEqual(expectedResult, methodResult);
        }

        [TestMethod()]
        public void SumTwoPositiveDecimalNumbersTest()
        {
            decimal expectedResult = 0;

            decimal methodResult = calculator.SubtractTwoNumbers(0.1m, 0.1m);

            Assert.AreEqual(expectedResult, methodResult);
        }

        [TestMethod()]
        public void SumTwoNegativeDecimalNumbersTest()
        {
            decimal expectedResult = 0;

            decimal methodResult = calculator.SubtractTwoNumbers(-0.1m, -0.1m);

            Assert.AreEqual(expectedResult, methodResult);
        }
    }
}
