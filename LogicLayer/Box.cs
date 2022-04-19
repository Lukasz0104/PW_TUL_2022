using System;
using System.Collections.Generic;
using System.Text;

namespace LogicLayer
{
    public class Box
    {
        public double SizeX { get; }
        public double SizeY { get; }

        private List<Ball> balls = new List<Ball>();
        public List<Ball> Balls { get => balls; }

        public Box(double sizeX, double sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            balls = new List<Ball>();
        }

        public void addBall(Ball b)
        { 
            Balls.Add(b);
        }
    }
}
