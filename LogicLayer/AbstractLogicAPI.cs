using System;
using System.Collections.Generic;
using System.Threading;
using DataLayer;

namespace LogicLayer
{
    public abstract class AbstractLogicAPI
    {
        private readonly AbstractDataAPI dataAPI;

        public AbstractLogicAPI(AbstractDataAPI dataAPI = null)
        {
            this.dataAPI = dataAPI ?? AbstractDataAPI.createDataAPI();
        }

        public abstract List<BallWrapper> GetBalls();

        public abstract void createBall();
        public abstract void createBalls(int count);

        public abstract void start();
        public abstract void stop();

        public static AbstractLogicAPI createLogicAPI(AbstractDataAPI dataAPI = null)
        {
            return new LogicAPI(dataAPI);
        }

        private class LogicAPI : AbstractLogicAPI
        {
            private readonly Random random = new Random();
            private bool isRunning = false;
            private List<Thread> ballThreads = new List<Thread>();
            private List<BallWrapper> balls = new List<BallWrapper>();

            private double width;
            private double height;

            private Barrier barrier = new Barrier(0);

            public LogicAPI(AbstractDataAPI abstractDataAPI = null)
                : base(abstractDataAPI) 
            {
                width = dataAPI.Box.SizeX;
                height = dataAPI.Box.SizeY;
            }

            public override List<BallWrapper> GetBalls() => balls;

            public override void createBall()
            {
                Ball b =  dataAPI.createBall();
                int angle = random.Next(360);
                double vx = 2.5 * Math.Sin(angle * Math.PI / 180);
                double vy = 2.5 * Math.Cos(angle * Math.PI / 180);
                BallWrapper bw = new BallWrapper(b, vx, vy);
                balls.Add(bw);

                barrier.AddParticipant();

                Thread t = new Thread(() =>
                {
                    while (bw.Active)
                    {
                        bw.update();
                        if ((bw.Radius + bw.PositionX) > width)
                        {
                            bw.VelocityX *= -1;
                            bw.PositionX = width - Math.Abs(bw.PositionX - width);
                        }
                        else if (bw.PositionX < bw.Radius)
                        {
                            bw.VelocityX *= -1;
                            bw.PositionX = bw.Radius + Math.Abs(bw.Radius - bw.PositionX);
                        }

                        if ((bw.PositionY + bw.Radius) > height)
                        {
                            bw.VelocityY *= -1;
                            bw.PositionY = height - Math.Abs(bw.PositionY - height);
                        }
                        else if (bw.PositionY < bw.Radius)
                        {
                            bw.VelocityY *= -1;
                            bw.PositionY = bw.Radius + Math.Abs(bw.Radius - bw.PositionY);
                        }
                        Thread.Sleep(10);
                        barrier.SignalAndWait();
                    }
                });
                if (isRunning)
                {
                    t.Start();
                }
                ballThreads.Add(t);
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

            ~LogicAPI()
            {
                isRunning = false;
                foreach (BallWrapper bw in balls)
                {
                    bw.stop();
                }
            }

        }
    }
}
