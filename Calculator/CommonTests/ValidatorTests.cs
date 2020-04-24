using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Tests
{
    [TestClass()]
    public class ValidatorTests
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
}