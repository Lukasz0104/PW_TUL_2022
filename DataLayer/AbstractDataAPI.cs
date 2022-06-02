using System;

namespace DataLayer
{
    public abstract class AbstractDataAPI
    {
        private Logger _logger = new Logger();

        private readonly Box box;
        public Box Box { get => box; }

        public static AbstractDataAPI createDataAPI()
        {
            return new DataAPI();
        }

        public AbstractDataAPI(double width = 450.0, double height = 300.0)
        {
            box = new Box(width, height);
        }

        public abstract Ball createBall();

        public void StartLogging()
        {
            _logger.Log(box.Balls);
        }

        public void StopLogging()
        {
            _logger.Stop();
        }

        ~AbstractDataAPI() => StopLogging();


        private class DataAPI : AbstractDataAPI
        {
            private readonly Random random = new Random();
            public DataAPI(double width = 450.0, double height = 300.0) 
                : base(width, height) { }

            public override Ball createBall()
            {                
                double r = 20.0;
                double x = r + random.NextDouble() * (box.SizeX - 2 * r);
                double y = r + random.NextDouble() * (box.SizeY - 2 * r);
                bool f = true;
                do
                {
                    f = true;

                    foreach (Ball b1 in box.Balls)
                    {
                        double dx = x - b1.PositionX;
                        double dy = y - b1.PositionY;
                        double distance = Math.Sqrt(dx * dx + dy * dy);
                        if (distance <= (r + b1.Radius))
                        {
                            f = false;
                            x = r + random.NextDouble() * (box.SizeX - 2 * r);
                            y = r + random.NextDouble() * (box.SizeY - 2 * r);
                            break;
                        }
                    }
                } while (!f);

                Ball b = new Ball(x, y, r);
                box.addBall(b);
                return b;
            }
        }
    }
}
