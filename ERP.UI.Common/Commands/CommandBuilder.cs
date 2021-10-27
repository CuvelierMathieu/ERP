using System;
using System.Windows.Input;

namespace ERP.UI.Common.Commands
{
    public static class CommandBuilder
    {
        public static ICommand BuildWithParameter<T>(Action<T?> execute)
        {
            return BuildWithParameter(execute, null);
        }

        public static ICommand BuildWithParameter<T>(Action<T?> execute, Func<T?, bool>? canExecute)
        {
            return new Command<T>(execute, canExecute);
        }

        public static ICommand Build(Action execute)
        {
            return Build(execute, null);
        }

        public static ICommand Build(Action execute, Func<bool>? canExecute)
        {
            return new Command(execute, canExecute);
        }
    }
}
