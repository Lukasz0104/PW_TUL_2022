using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataLayer
{
    public class Ball : INotifyPropertyChanged
    {
        private double positionX;
        private double positionY;
        private readonly double radius;
        private double velocityX;
        private double velocityY;

        public double PositionX 
        {
            get => positionX;
            set
            {
                positionX = value;
                RaisePropertyChanged();
            }
        }

        public double PositionY
        {
            get => positionY;
            set
            {
                positionY = value;
                RaisePropertyChanged();
            }
        }

        public double Radius { get => radius; }

        public double VelocityX { get => velocityX; set => velocityX = value; }
        public double VelocityY { get => velocityY; set=> velocityY = value; }

        public Ball(double positionX, double positionY, double radius, double velocityX, double velocityY)
        {
            this.positionX = positionX;
            this.positionY = positionY;
            this.radius = radius;
            this.velocityX = velocityX;
            this.velocityY = velocityY;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void updateBall()
        {
            PositionX += velocityX;
            PositionY += velocityY;
        }
    }
}
