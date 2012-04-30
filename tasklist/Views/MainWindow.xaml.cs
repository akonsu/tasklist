using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TaskList.Views
{
    public partial class MainWindow : Window
    {
        static T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            var parent = element;

            while (parent != null)
            {
                T t = parent as T;

                if (t != null)
                {
                    return t;
                }
                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var cell = sender as DataGridCell;

            if (cell != null && !cell.IsEditing && !cell.IsReadOnly)
            {
                if (!cell.IsFocused)
                {
                    cell.Focus();
                }

                var grid = FindVisualParent<DataGrid>(cell);

                if (grid != null)
                {
                    if (grid.SelectionUnit != DataGridSelectionUnit.FullRow)
                    {
                        if (!cell.IsSelected)
                        {
                            cell.IsSelected = true;
                        }
                    }
                    else
                    {
                        var row = FindVisualParent<DataGridRow>(cell);

                        if (row != null && !row.IsSelected)
                        {
                            row.IsSelected = true;
                        }
                    }
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            this.CompleteTasks.CurrentCellChanged += (s, e) => this.CompleteTasks.CommitEdit();
            this.IncompleteTasks.CurrentCellChanged += (s, e) => this.IncompleteTasks.CommitEdit();
        }
    }
}
