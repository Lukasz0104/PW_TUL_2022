using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer;

namespace LogicLayerTest
{

    [TestClass]
    public  class BallTest
    {
        [TestMethod]
        public void TestUpdateBall()
        {
            Ball b = new Ball(0.0, 0.0, 10.0, 1.5, -1.0);
            b.updateBall();
            Assert.AreEqual(1.5, b.PositionX);
            Assert.AreEqual(-1.0, b.PositionY);
        }
    }
}
