using System;
using System.Collections.Generic;
using System.Threading;
using DataLayer;

namespace LogicLayer
{
    public abstract class AbstractLogicAPI
    {
        private AbstractDataAPI dataAPI;

        public AbstractLogicAPI(AbstractDataAPI dataAPI = null)
        {
            this.dataAPI = (dataAPI == null) ? AbstractDataAPI.createDataAPI() : dataAPI;
        }

        public abstract IEnumerable<Ball> GetBalls();

        public abstract void createBall();
        public abstract void createBalls(int count);
        public abstract void deleteBall();

        public abstract void start();
        public abstract void stop();

        public static AbstractLogicAPI createLogicAPI(AbstractDataAPI dataAPI = null)
        {
            return new LogicAPI(150.0, 100.0, dataAPI);
        }

        private class LogicAPI : AbstractLogicAPI
        {
            private bool isRunning = false;
            private Random random = new Random();
            private Box box;
            private List<Thread> ballThreads = new List<Thread> ();

            public LogicAPI(double sizeX, double sizeY, AbstractDataAPI abstractDataAPI = null)
                : base(abstractDataAPI)
            {
                box = new Box(sizeX, sizeY);
            }

            ~LogicAPI()
            {
                foreach (Thread t in ballThreads)
                {
                    t.Abort();
                }
            }

            public override IEnumerable<Ball> GetBalls()
            {
                return box.Balls;
            }

            public override void createBall()
            {
                double r = 20.0;
                double x = r + random.NextDouble() * (box.SizeX - 2 * r);
                double y = r + random.NextDouble() * (box.SizeY - 2 * r);
                int angle = random.Next(360);
                double vx = 1 * Math.Sin(angle * Math.PI / 180);
                double vy = 1 * Math.Cos(angle * Math.PI / 180);
                Ball b = new Ball(x, y, r, vx, vy);

                box.addBall(b);
                Thread t = new Thread(() =>
                {
                    while (isRunning)
                    {
                        b.updateBall();
                        Thread.Sleep(1000);
                    }
                });
                ballThreads.Add(t);
            }

            public override void deleteBall()
            {
                box.Balls.RemoveAt(box.Balls.Count - 1);
                ballThreads[ballThreads.Count - 1].Abort();
                ballThreads.RemoveAt(ballThreads.Count - 1);
            }

            public override void createBalls(int count)
            {
                for (int i = 0; i < count; i++)
                {
                    createBall();
                }
            }

            public override void start()
            {
                if (!isRunning)
                {
                    isRunning = true;
                    foreach (Thread thread in ballThreads)
                    {
                        thread.Start();
                    }
                }
            }

            public override void stop()
            {
                isRunning = false;
            }

        }
    }
}
