using System;
using System.Collections.Generic;
using System.Text;

namespace LogicLayer
{
    public class Box
    {
        public double SizeX { get; }
        public double SizeY { get; }

        public List<Ball> Balls { get; }

        public Box(double sizeX, double sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            Balls = new List<Ball>();
        }

        public void addBall(Ball b)
        { 
            Balls.Add(b);
        }
    }
}
