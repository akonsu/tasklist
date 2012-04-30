using System.Windows;

namespace TaskList.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.CompleteTasks.CurrentCellChanged += (s, e) => this.CompleteTasks.CommitEdit();
            this.IncompleteTasks.CurrentCellChanged += (s, e) => this.IncompleteTasks.CommitEdit();
        }
    }
}
