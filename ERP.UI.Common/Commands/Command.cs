using System;
using System.Windows.Input;

namespace ERP.UI.Common.Commands
{
    public class Command : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public Command(Action execute, Func<bool>? canExecute)
        {
            _execute = execute;
            _canExecute = canExecute ?? CanAlwaysExecute;
        }

        public bool CanExecute(object? parameter) => _canExecute();

        public void Execute(object? parameter) => _execute();

        private static bool CanAlwaysExecute() => true;
    }
}
