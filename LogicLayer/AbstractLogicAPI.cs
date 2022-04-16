using System;
using DataLayer;

namespace LogicLayer
{
    public abstract class AbstractLogicAPI
    {
        private AbstractDataAPI dataAPI;

        public AbstractLogicAPI(AbstractDataAPI dataAPI)
        {
            this.dataAPI = dataAPI;
        }

        public abstract void createBall();
        public abstract void createBalls(int count);
        public abstract void deleteBall();

        public static AbstractLogicAPI createLogicAPI(AbstractDataAPI dataAPI = null)
        {
            return new LogicAPI(150.0, 100.0, dataAPI == null ? AbstractDataAPI.createDataAPI() : dataAPI);
        }

        private class LogicAPI : AbstractLogicAPI
        {
            private Random random = new Random();
            private Box box;

            public LogicAPI(double sizeX, double sizeY, AbstractDataAPI abstractDataAPI = null)
                : base(abstractDataAPI)
            {
                box = new Box(sizeX, sizeY);
            }

            public override void createBall()
            {
                double r = 5.0;
                double x = r + random.NextDouble() * (box.SizeX - 2 * r);
                double y = r + random.NextDouble() * (box.SizeY - 2 * r);
                box.addBall(new Ball(x, y, r));
            }

            public override void deleteBall()
            {
                box.Balls.RemoveAt(box.Balls.Count - 1);
            }

            public override void createBalls(int count)
            {
                for (int i = 0; i < count; i++)
                {
                    createBall();
                }
            }
        }
    }
}
