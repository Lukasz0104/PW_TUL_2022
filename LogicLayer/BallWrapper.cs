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
        
        private double positionX;
        public double PositionX
        {
            get => positionX;
            set
            {
                positionX = value;
                //ball.PositionX = positionX;
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
                //ball.PositionY = positionY;
                RaisePropertyChanged();
            }
        }


        private double radius;
        public double Radius { get => radius; }
        public double VelocityX 
        {
            get => ball.VelocityX;
            set => ball.VelocityX = value;
        }

        public double VelocityY
        {
            get => ball.VelocityX;
            set => ball.VelocityY = value;
        }

        private bool active;
        public bool Active { get => active; }

        public BallWrapper(Ball b, bool active = true)
        {
            b.PropertyChanged += OnPropertyChanged;
            ball = b;
            this.active = active;
            positionX = b.PositionX;
            positionY = b.PositionY;
            radius = b.Radius;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public void update()
        {
            ball.updateBall();
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
