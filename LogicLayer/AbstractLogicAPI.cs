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

            private object _lock = new object();

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
                double velocityMagnitude = 1.5;
                double vx =  Math.Sin(angle * Math.PI / 180) * velocityMagnitude;
                double vy = Math.Cos(angle * Math.PI / 180) * velocityMagnitude;
                BallWrapper bw = new BallWrapper(b, vx, vy);
                balls.Add(bw);

                Thread t = new Thread(() =>
                {
                    while (bw.Active)
                    {
                        bw.Moved = false;
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

                        Thread.Sleep(5);

                        bw.update();
                        if (!bw.Moved)
                        {
                            detectCollisions(bw);
                        }
                    }
                });
                if (isRunning)
                {
                    t.Start();
                }
                ballThreads.Add(t);
            }

            private void detectCollisions(BallWrapper ball)
            {
                lock (_lock)
                {
                    double px = ball.PositionX;
                    double py = ball.PositionY;
                    foreach (BallWrapper b in balls)
                    {
                        if (b == ball) continue;

                        if (b.Moved) continue;

                        double dx = px - b.PositionX;
                        double dy = py - b.PositionY;
                        double massSum = ball.Mass + b.Mass;

                        if (Math.Sqrt(dx * dx + dy * dy) <= (ball.Radius + b.Radius))
                        {
                            double v1x = ball.VelocityX, v1y = ball.VelocityY, v2x = b.VelocityX, v2y = b.VelocityY;

                            ball.Moved = true;
                            b.Moved = true;

                            double dvx = v1x - b.VelocityX;
                            double dvy = v1y - b.VelocityY;

                            double c = 2 * (dx * dvx + dy * dvy) / (dx * dx + dy * dy) / massSum;

                            ball.VelocityX = v1x - b.Mass * dx * c;
                            ball.VelocityY = v1y - b.Mass * dy * c;

                            b.VelocityX = v2x + ball.Mass * dx * c;
                            b.VelocityY = v2y + ball.Mass * dy * c;
                        }
                    }
                }
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
                    dataAPI.StartLogging();
                }
            }

            public override void stop()
            {
                isRunning = false;
                dataAPI.StopLogging();
            }

            ~LogicAPI()
            {
                isRunning = false;
                foreach (BallWrapper bw in balls)
                {
                    bw.stop();
                }
                stop();
            }

        }
    }
}
