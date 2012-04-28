using System;
using System.Windows.Input;
using TaskList.Models;

namespace LogSafe.Commands
{
    internal class CreateTaskCommand : ICommand
    {
        private readonly MainViewModel model;

        public CreateTaskCommand(MainViewModel model)
        {
            this.model = model;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            this.model.Tasks.Add(new TaskModel(this.model.NewTitle));
        }
    }
}
