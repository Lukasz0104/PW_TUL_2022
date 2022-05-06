using System;
using System.Windows.Input;

namespace PresentationLayer.ViewModelLayer
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> m_Execute;
        private readonly Predicate<object> m_CanExecute;

        public RelayCommand(Action<object> execute) : this(execute, null) { }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            m_Execute = execute;
            m_CanExecute = canExecute;
        }
        
        public bool CanExecute(object parameter)
        {
            return m_CanExecute == null || m_CanExecute(parameter);
        }

        public virtual void Execute(object parameter)
        {
            m_Execute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        internal void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
