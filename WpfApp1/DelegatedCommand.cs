using System;
using System.Windows.Input;

namespace WpfApp1
{
    public class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private readonly Action _executeMethod;
        private readonly Func<bool> _canExecuteMethod;

        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute()
        {
            if (_canExecuteMethod != null)
            {
                return _canExecuteMethod();
            }
            return true;
        }

        public void Execute()
        {
            if (_executeMethod != null)
            {
                _executeMethod();
            }
        }
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            Execute();
        }
    }

    public class DelegateCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        private readonly Action<T> _executeMethod;
        private readonly Func<T, bool> _canExecuteMethod;

        #region Constructors
        public DelegateCommand(Action<T> executeMethod)
        {
            _executeMethod = executeMethod;
        }
        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }
        #endregion

        #region Public Methods
        public bool CanExecute(T parameter)
        {
            if (_canExecuteMethod != null)
            {
                return _canExecuteMethod(parameter);
            }
            return true;
        }

        public void Execute(T parameter)
        {
            if (_executeMethod != null)
            {
                _executeMethod(parameter);
            }
        }
        #endregion

        #region ICommand Members

        bool ICommand.CanExecute(object parameter)
        {
            if (parameter == null &&
                typeof(T).IsValueType)
            {
                return _canExecuteMethod == null;
            }
            return CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            Execute((T)parameter);
        }

        #endregion
    }
}
