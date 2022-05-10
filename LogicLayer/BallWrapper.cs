using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using DataLayer;

namespace LogicLayer
{
    public class BallWrapper : INotifyPropertyChanged
    {
        private readonly Ball ball;
        private double velocityX;
        private double velocityY;
        
        private double positionX;
        public double PositionX
        {
            get => positionX;
            set
            {
                positionX = value;
                RaisePropertyChanged();
            }
        }

        private double positionY;
        public double PositionY
        {
            get => positionY;
            set
            {
                positionY = value;
                RaisePropertyChanged();
            }
        }


        private double radius;
        public double Radius { get => radius; }
        public double VelocityX 
        {
            get => velocityX;
            set => velocityX = value;
        }

        public double VelocityY
        {
            get => velocityX;
            set => velocityY = value;
        }

        private bool active;
        public bool Active { get => active; }

        public BallWrapper(Ball b, double vx, double vy, bool active = true)
        {
            b.PropertyChanged += OnPropertyChanged;
            ball = b;
            this.active = active;
            positionX = b.PositionX;
            positionY = b.PositionY;
            radius = b.Radius;

            velocityX = vx;
            velocityY = vy;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public void update()
        {
            ball.updateBall(velocityX, velocityY);
        }

        public void start()
        {
            active = true;
        }

        public void stop()
        {
            active = false;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Ball b = (Ball)sender;
            switch (e.PropertyName)
            {
                case "PositionX":
                    PositionX = b.PositionX;
                    break;
                case "PositionY":
                    PositionY = b.PositionY;
                    break;
            }
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
