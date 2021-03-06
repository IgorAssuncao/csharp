using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathCustom.Tests
{
    [TestClass()]
    public class CalculatorTests
    {
        public TestContext TestContext { get; set; }
        private static TestContext _testContext;

        // // Run before all tests
        // [ClassInitialize]
        // public static void InitializeTestClass(TestContext testContext)
        // {
        //     _testContext = testContext;
        // }

        // // Run after all tests
        // [ClassCleanup]
        // public static void CleanUpTestClass()
        // {
        //     _testContext = null;
        // }

        // Run before each test
        // [TestInitialize]
        // public void SetupTest()
        // {
        //     Calculator = new Calculator();

        //     // por algum motivo essa linha quebra os testes com um NullPointerException
        //     // arrisco que seja pelo fato dos TestMethods estarem dentro de cada classe especifica
        //     // e nao dentro dessa classe de setup
        //     // ToTry: Tambem colocar o SetupTest dentro de cada classe que herda dessa classe para
        //     // entender o comportamento

        //     // Console.WriteLine("TestContext.TestName='{0}'", _testContext.TestName);
        // }
    }

    [TestClass()]
    public class CalculatorSumTests : CalculatorTests
    {
        [TestMethod()]
        public void SumTwoPositiveNumbersTest()
        {
            decimal expectedResult = 2;

            decimal methodResult = Calculator.SumTwoNumbers(1, 1);

            Assert.AreEqual(expectedResult, methodResult);
        }

        [TestMethod()]
        public void SumTwoNegativeNumbersTest()
        {
            decimal expectedResult = -2;

            decimal methodResult = Calculator.SumTwoNumbers(-1, -1);

            Assert.AreEqual(expectedResult, methodResult);
        }

        [TestMethod()]
        public void SumTwoPositiveDecimalNumbersTest()
        {
            decimal expectedResult = 0.2m;

            decimal methodResult = Calculator.SumTwoNumbers(0.1m, 0.1m);

            Assert.AreEqual(expectedResult, methodResult);
        }

        [TestMethod()]
        public void SumTwoNegativeDecimalNumbersTest()
        {
            decimal expectedResult = -0.2m;

            decimal methodResult = Calculator.SumTwoNumbers(-0.1m, -0.1m);

            Assert.AreEqual(expectedResult, methodResult);
        }
    }

    [TestClass()]
    public class CalculatorSubtractTests : CalculatorTests
    {
        [TestMethod()]
        public void SubtractTwoPositiveNumbersTest()
        {
            decimal expectedResult = 0;

            decimal methodResult = Calculator.SubtractTwoNumbers(1, 1);

            Assert.AreEqual(expectedResult, methodResult);
        }

        [TestMethod()]
        public void SubtractTwoNegativeNumbersTest()
        {
            decimal expectedResult = 0;

            decimal methodResult = Calculator.SubtractTwoNumbers(-1, -1);

            Assert.AreEqual(expectedResult, methodResult);
        }

        [TestMethod()]
        public void SumTwoPositiveDecimalNumbersTest()
        {
            decimal expectedResult = 0;

            decimal methodResult = Calculator.SubtractTwoNumbers(0.1m, 0.1m);

            Assert.AreEqual(expectedResult, methodResult);
        }

        [TestMethod()]
        public void SumTwoNegativeDecimalNumbersTest()
        {
            decimal expectedResult = 0;

            decimal methodResult = Calculator.SubtractTwoNumbers(-0.1m, -0.1m);

            Assert.AreEqual(expectedResult, methodResult);
        }
    }
}