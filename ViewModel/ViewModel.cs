using System;
using System.ComponentModel;

namespace ViewModelLayer
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
