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
            return new LogicAPI(450, 300.0, dataAPI);
        }

        private class LogicAPI : AbstractLogicAPI
        {
            private bool isRunning = false;
            private List<Thread> ballThreads = new List<Thread>();
            private List<BallWrapper> balls = new List<BallWrapper>();

            public LogicAPI(double sizeX, double sizeY, AbstractDataAPI abstractDataAPI = null)
                : base(abstractDataAPI) { }

            public override List<BallWrapper> GetBalls() => balls;

            public override void createBall()
            {
                Ball b =  dataAPI.createBall();
                BallWrapper bw = new BallWrapper(b);
                balls.Add(bw);

                Thread t = new Thread(() =>
                {
                    while (bw.Active)
                    {
                        // TODO prevent balls from going beyond bottom border
                        bw.update();
                        if ((bw.Radius + bw.PositionX) >= dataAPI.Box.SizeX)
                        {
                            bw.VelocityX = -bw.VelocityX;
                            bw.PositionX = dataAPI.Box.SizeX - Math.Abs(bw.PositionX - dataAPI.Box.SizeX);
                        }
                        else if (bw.PositionX <= bw.Radius)
                        {
                            bw.VelocityX = -bw.VelocityX;
                            bw.PositionX = bw.Radius + Math.Abs(bw.Radius - bw.PositionX);
                        }

                        if ((bw.PositionY + bw.Radius) >= dataAPI.Box.SizeY)
                        {
                            bw.VelocityY = -bw.VelocityY;
                            bw.PositionY = dataAPI.Box.SizeY - Math.Abs(bw.PositionY - dataAPI.Box.SizeY);
                        }
                        if (bw.PositionY <= bw.Radius)
                        {
                            bw.VelocityY = -bw.VelocityY;
                            bw.PositionY = bw.Radius + Math.Abs(bw.Radius - bw.PositionY);
                        }
                        Thread.Sleep(10);
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
                    //foreach (BallWrapper bw in balls)
                    //{
                    //    bw.start();
                    //}
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
