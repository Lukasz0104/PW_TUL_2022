using System.Collections.Generic;

namespace LogicLayer
{
    public class Box
    {
        private readonly double sizeX;
        private readonly double sizeY;

        public double SizeX { get => sizeX; }
        public double SizeY { get => sizeY; }

        private List<Ball> balls = new List<Ball>();
        public List<Ball> Balls { get => balls; }

        public Box(double sizeX, double sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            balls = new List<Ball>();
        }

        public void addBall(Ball b)
        { 
            balls.Add(b);
        }
    }
}
