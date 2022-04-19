using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ModelLayer;

namespace ViewModelLayer
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<BallAdapter> Balls { get => modelAPI.ObservableBallCollection; }
        public ICommand StartCommand { get; set; }
        public ICommand AddBallCommand { get; set; }
        public ICommand RemoveBallCommand { get; set; }

        public ViewModel()
        {
            StartCommand = new RelayCommand(Start);
            AddBallCommand = new RelayCommand(AddBall);
            RemoveBallCommand = new RelayCommand(RemoveBall);
        }

        private Model modelAPI = new Model();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Start(object obj)
        {
            modelAPI.start();
        }

        private void AddBall(object obj)
        {
            modelAPI.addBall();
        }

        private void RemoveBall(object obj)
        {
            modelAPI.removeBall();
        }
    }
}
