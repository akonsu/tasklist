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

            var window = new MainWindow();

            window.DataContext = new MainViewModel();
            window.Show();
        }
    }
}
