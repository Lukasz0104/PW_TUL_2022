using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PresentationLayer.ModelLayer;

namespace PresentationLayer.ViewModelLayer
{
    public class ViewModel : INotifyPropertyChanged
    {
        private int ballCountInt = 2;
        private Model modelAPI = new Model();

        public ObservableCollection<BallAdapter> Balls
        {
            get => modelAPI.ObservableBallCollection;
            set
            {
                modelAPI.ObservableBallCollection = value;
            }
        }

        public string BallCount
        {
            get => Convert.ToString(ballCountInt);
            set
            {
                try
                {
                    int t = Convert.ToInt32(value);
                    if (t > 0)
                    {
                        ballCountInt = t;
                    }
                }
                catch (FormatException)
                {
                    ballCountInt = 2;
                }
            }
        }

        public ICommand StartCommand { get; set; }
        public ICommand AddBallCommand { get; set; }
        public ICommand RemoveBallCommand { get; set; }

        public ViewModel()
        {
            StartCommand = new RelayCommand(Start);
            AddBallCommand = new RelayCommand(AddBall);
            RemoveBallCommand = new RelayCommand(RemoveBall);
        }


        private void Start(object obj)
        {
            if (modelAPI.ObservableBallCollection.Count == 0)
            {
                modelAPI.createBalls(ballCountInt);
            }
            modelAPI.start();
        }

        private void AddBall(object obj)
        {
            ballCountInt++; ;
            modelAPI.addBall();
            RaisePropertyChanged();
        }

        private void RemoveBall(object obj)
        {
            modelAPI.removeBall();
            ballCountInt--;
            RaisePropertyChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
