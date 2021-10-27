using System;
using System.Windows.Input;

namespace ERP.UI.Common.Commands
{
    public class Command<T> : ICommand
    {
        private readonly Action<T?> _execute;
        private readonly Func<T?, bool> _canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public Command(Action<T?> execute, Func<T?, bool>? canExecute)
        {
            _execute = execute;
            _canExecute = canExecute ?? CanAlwaysExecute;
        }

        public bool CanExecute(object? parameter)
        {
            if (parameter is null)
                return _canExecute(default);

            return _canExecute((T)parameter);
        }

        public void Execute(object? parameter)
        {
            if (parameter is null)
                _execute(default);
            else
                _execute((T)parameter);
        }

        private static bool CanAlwaysExecute(T? parameter) => true;
    }
}
