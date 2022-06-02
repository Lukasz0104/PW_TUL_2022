using System;

namespace DataLayer
{
    internal class BallDetails
    {
        private readonly string currentTimeStamp;
        private readonly Ball ball;
        private int id;

        public string Timestamp { get => currentTimeStamp; }
        public int Id { get => id; }
        public double X { get => ball.PositionX; }
        public double Y { get => ball.PositionY; }

        public BallDetails(int id, Ball b)
        {
            ball = b;
            currentTimeStamp = DateTime.Now.ToString();
            this.id = id;
        }
    }
}
