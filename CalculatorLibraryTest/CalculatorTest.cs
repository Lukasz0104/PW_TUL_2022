using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorLibrary;

namespace CalculatorLibraryTest
{
    [TestClass]
    public class CalculatorTest
    {
        Calculator calc = new Calculator();

        [TestMethod]
        public void TestAdd()
        {
            int result = calc.Add(1, 2);

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void TestSubtract()
        {
            int result = calc.Subtract(2, 1);

            Assert.AreEqual(1, result);
        }
    }
}