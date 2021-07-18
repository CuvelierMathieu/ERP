using System;
using System.Windows.Input;

namespace ERP.UI.Common.Commands
{
    public class Command<T> : ICommand
    {
        private Action<T> _execute;
        private Func<T, bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public Command(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute ?? CanAlwaysExecute;
        }

        public bool CanExecute(object parameter) => _canExecute((T)parameter);

        public void Execute(object parameter) => _execute((T)parameter);

        private static bool CanAlwaysExecute(T parameter) => true;
    }
}
