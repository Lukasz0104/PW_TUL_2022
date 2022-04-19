using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ViewModelLayer
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> m_Execute;
        private readonly Predicate<object> m_CanExecute;

        public RelayCommand(Action<object> execute) : this(execute, null) { }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.m_Execute = execute;
            this.m_CanExecute = canExecute;
        }
        
        public bool CanExecute(object parameter)
        {
            return m_CanExecute == null || m_CanExecute(parameter);
        }

        public virtual void Execute(object parameter)
        {
            this.m_Execute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        internal void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
