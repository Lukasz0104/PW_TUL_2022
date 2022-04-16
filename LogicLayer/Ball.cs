using System;
using System.Collections.Generic;
using System.Text;

namespace LogicLayer
{
    public class Ball
    {
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public double Radius { get; }

        public Ball(double positionX, double positionY, double radius)
        {
            PositionX = positionX;
            PositionY = positionY;
            Radius = radius;
        }

        public void updateBall(double changeX, double changeY)
        {
            PositionX += changeX;
            PositionY += changeY;
        }
    }
}
