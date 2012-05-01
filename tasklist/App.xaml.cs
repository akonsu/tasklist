using System.Windows;
using TaskList.Models;
using TaskList.Views;

namespace TaskList
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var main_window = new MainWindow();
            var notify_window = new NotifyWindow();

            main_window.DataContext = new MainViewModel();

            main_window.Show();
        }
    }
}
