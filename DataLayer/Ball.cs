using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataLayer
{
    public class Ball : INotifyPropertyChanged
    {
        private double positionX;
        private double positionY;
        private readonly double radius;
        private double mass = 5;
        
        public Ball(double positionX, double positionY, double radius)
        {
            this.positionX = positionX;
            this.positionY = positionY;
            this.radius = radius;
        }

        public double Mass { get => mass; }

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void updateBall(double velocityX, double velocityY)
        {
            PositionX += velocityX;
            PositionY += velocityY;
        }
    }
}
