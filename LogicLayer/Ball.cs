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

        public double VelocityX { get; set; }
        public double VelocityY { get; set; }

        public Ball(double positionX, double positionY, double radius, double velocityX, double velocityY)
        {
            PositionX = positionX;
            PositionY = positionY;
            Radius = radius;
            VelocityX = velocityX;
            VelocityY = velocityY;
        }

        public void updateBall()
        {
            PositionX += VelocityX;
            PositionY += VelocityY;
        }
    }
}
