using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;

namespace LogicLayerTest
{
    [TestClass]
    public class LogicTest
    {
        private readonly AbstractLogicAPI logic = AbstractLogicAPI.createLogicAPI();

        [TestMethod]
        public void TestCreateBall()
        {
            Assert.AreEqual(0, logic.GetBalls().Count);
            logic.createBall();
            Assert.AreEqual(1, logic.GetBalls().Count);
        }

        [TestMethod]
        public void TestCreateBalls()
        {
            logic.createBalls(5);
            Assert.AreEqual(5, logic.GetBalls().Count);
        }
    }
}