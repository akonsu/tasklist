using System.Windows;
using TaskList.Commands;
using TaskList.Models;
using TaskList.Views;

namespace TaskList
{
    public partial class App : Application
    {
        private void OnCloseCommand(object parameter)
        {
            this.Shutdown();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var main_window = new MainWindow();
            var notify_window = new NotifyWindow();

            var main_model = new MainViewModel();
            var notify_model = new NotifyVewModel();

            notify_model.CloseCommand = new DelegateCommand(this.OnCloseCommand);

            main_window.DataContext = main_model;
            notify_window.DataContext = notify_model;

            main_window.Show();
        }
    }
}
