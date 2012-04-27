using System.Windows;
using TaskList.Views;

namespace TaskList.Presentation
{
    public partial class MainWindow : Window, IMainView
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
