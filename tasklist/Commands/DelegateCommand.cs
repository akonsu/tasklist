using System;
using System.Windows.Input;

namespace TaskList.Commands
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> action;
        private readonly Predicate<object> predicate;

        public DelegateCommand(Action<object> action) : this(action, null) { }

        public DelegateCommand(Action<object> action, Predicate<object> predicate)
        {
            this.action = action;
            this.predicate = predicate;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return this.predicate == null || this.predicate(parameter);
        }

        public void Execute(object parameter)
        {
            this.action(parameter);
        }
    }
}
