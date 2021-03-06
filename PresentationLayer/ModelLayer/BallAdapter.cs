using System.ComponentModel;
using System.Runtime.CompilerServices;
using LogicLayer;

namespace PresentationLayer.ModelLayer
{
    public class BallAdapter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private double radius;
        private double positionX;
        private double positionY;

        public BallAdapter(BallWrapper ball)
        {
            ball.PropertyChanged += OnPropertyChanged;
            radius = ball.Radius;
            positionX = ball.PositionX;
            positionY = ball.PositionY;
        }

        public double Radius
        {
            get => radius;
            set => radius = value;
        }

        public double Diameter
        {
            get => 2 * radius;
        }

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

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public double CenterTransform => -radius;

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            BallWrapper b = (BallWrapper)sender;
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
    }
}
