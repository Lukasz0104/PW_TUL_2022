﻿using System;
using System.Collections.Generic;

namespace DataLayer
{
    public abstract class AbstractDataAPI
    {
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
                Ball b = new Ball(x, y, r);
                box.addBall(b);
                return b;
            }
        }
    }
}
