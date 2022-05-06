using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer;

namespace DataLayerTest
{
    [TestClass]
    public class BoxTest
    {
        [TestMethod]
        public void AddBallTest()
        {
            Box box = new Box(100, 100);
            Ball b = new Ball(50, 50, 5, 1, 1);

            Assert.AreEqual(box.Balls.Count, 0);

            box.addBall(b);

            Assert.AreEqual(box.Balls.Count, 1);
        }
    }
}