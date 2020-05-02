using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Tests
{
    [TestClass()]
    public class ValidatorTests
    {

    }

    [TestClass()]
    public class ValidateIntegersFromString : ValidatorTests
    {
        [TestMethod()]
        public void ValidateIntegersFromStringTestWhenEmptyStringIsGiven()
        {
            bool methodResult = Validator.ValidateIntegersFromString("");

            Assert.IsFalse(methodResult);
        }

        [TestMethod()]
        public void ValidateIntegersFromStringTestWhenAlphaStringIsGiven()
        {
            bool methodResult = Validator.ValidateIntegersFromString("abc");

            Assert.IsFalse(methodResult);
        }

        [TestMethod()]
        public void ValidateIntegersFromStringTestWhenAlphaNumericStringAbc123IsGiven()
        {
            bool methodResult = Validator.ValidateIntegersFromString("abc123");

            Assert.IsFalse(methodResult);
        }

        [TestMethod()]
        public void ValidateIntegersFromStringTestWhenAlphaNumericString123AbcIsGiven()
        {
            bool methodResult = Validator.ValidateIntegersFromString("123abc");

            Assert.IsFalse(methodResult);
        }

        [TestMethod()]
        public void ValidateIntegersFromStringTestWhenAlphaNumericStringAbc123AbcIsGiven()
        {
            bool methodResult = Validator.ValidateIntegersFromString("abc123abc");

            Assert.IsFalse(methodResult);
        }

        [TestMethod()]
        public void ValidateIntegersFromStringTestWhenNumericStringIsGiven()
        {
            bool methodResult = Validator.ValidateIntegersFromString("123");

            Assert.IsTrue(methodResult);
        }
    }

    [TestClass()]
    public class ValidateDecimalsFromString : ValidatorTests
    {
        [TestMethod()]
        public void ValidateDecimalsFromStringTestWhenEmptyStringIsGiven()
        {
            bool methodResult = Validator.ValidateDecimalsFromString("");

            Assert.IsFalse(methodResult);
        }

        [TestMethod()]
        public void ValidateDecimalsFromStringTestWhenAlphaStringIsGiven()
        {
            bool methodResult = Validator.ValidateDecimalsFromString("abc");

            Assert.IsFalse(methodResult);
        }

        [TestMethod()]
        public void ValidateDecimalsFromStringTestWhenAlphaNumericStringAbc123IsGiven()
        {
            bool methodResult = Validator.ValidateDecimalsFromString("abc123");

            Assert.IsFalse(methodResult);
        }

        [TestMethod()]
        public void ValidateDecimalssFromStringTestWhenAlphaNumericString123AbcIsGiven()
        {
            bool methodResult = Validator.ValidateDecimalsFromString("123abc");

            Assert.IsFalse(methodResult);
        }

        [TestMethod()]
        public void ValidateDecimalsFromStringTestWhenAlphaNumericStringAbc123AbcIsGiven()
        {
            bool methodResult = Validator.ValidateDecimalsFromString("abc123abc");

            Assert.IsFalse(methodResult);
        }

        [TestMethod()]
        public void ValidateDecimalsFromStringTestWhenNumericStringIsGiven()
        {
            bool methodResult = Validator.ValidateDecimalsFromString("123");

            Assert.IsTrue(methodResult);
        }
    }
}