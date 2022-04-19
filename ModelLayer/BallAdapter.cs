using System;
using System.Collections.Generic;
using System.Text;
using LogicLayer;

namespace ModelLayer
{
    public class BallAdapter
    {
        public Ball ball { get; }

        public BallAdapter(Ball ball)
        {
            this.ball = ball;
        }

        public double Radius { get => ball.Radius; }
        public double PositionX { get => ball.PositionX; }
        public double PositionY { get => ball.PositionY; }
    }
}
