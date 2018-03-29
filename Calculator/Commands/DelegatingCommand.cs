using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator.Commands
{
    public class DelegatingCommand : ICommand
    {
        // backing fields hold the delegates
        private readonly Action<object> _action;
        private readonly Func<object, bool> _canExecute;

        // forwarding c'tors
        public DelegatingCommand(Action action)
            : this(obj => action()) { }
        public DelegatingCommand(Action<object> action)
            : this(action, obj => true) { }

        // save arguments to backing fields
        public DelegatingCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _action = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        // invoked when CommandManager.RequerySuggested event is raises
        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        // invocation of delegated command
        public void Execute(object parameter)
        {
            this._action(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;

            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
