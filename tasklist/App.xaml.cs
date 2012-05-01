using System.Windows;
using TaskList.Commands;
using TaskList.Models;
using TaskList.Views;

namespace TaskList
{
    public partial class App : Application
    {
        private MainWindow main_window;
        private NotifyWindow notify_window;

        private bool CanViewTasksExecute(object parameter)
        {
            return !this.main_window.IsActive;
        }

        private void OnCloseCommand(object parameter)
        {
            this.notify_window.Dispose();
            this.Shutdown();
        }

        private void OnViewTasksCommand(object parameter)
        {
            this.main_window.Activate();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var main_model = new MainViewModel();
            var notify_model = new NotifyVewModel();

            notify_model.CloseCommand = new DelegateCommand(this.OnCloseCommand);
            notify_model.ViewTasksCommand = new DelegateCommand(this.OnViewTasksCommand,this.CanViewTasksExecute);

            this.main_window = new MainWindow();
            this.notify_window = new NotifyWindow();

            this.main_window.DataContext = main_model;
            this.notify_window.DataContext = notify_model;

            this.main_window.Show();
        }
    }
}
