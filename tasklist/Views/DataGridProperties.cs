using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TaskList.Views
{
    public static class DataGridProperties
    {
        //
        // this property attaches a command to DataGrid that is invoked when DataGrid.RowEditEnding event fires
        //
        private static readonly DependencyProperty RowEditEndingCommand
            = DependencyProperty.RegisterAttached("RowEditEndingCommand",
                                                  typeof(ICommand),
                                                  typeof(DataGridProperties),
                                                  new PropertyMetadata(new PropertyChangedCallback(OnRowEditEndingCommandChanged)));

        private static void OnRowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var d = sender as DependencyObject;
            var c = d != null ? DataGridProperties.GetRowEditEndingCommand(d) : null;

            if (c != null && c.CanExecute(d))
            {
                c.Execute(d);
            }
        }

        private static void OnRowEditEndingCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var g = d as DataGrid;

            if (g != null)
            {
                if (e.OldValue == null && e.NewValue != null)
                {
                    g.RowEditEnding += DataGridProperties.OnRowEditEnding;
                }
                else if (e.OldValue != null && e.NewValue == null)
                {
                    g.RowEditEnding -= DataGridProperties.OnRowEditEnding;
                }
            }
        }

        public static ICommand GetRowEditEndingCommand(DependencyObject d)
        {
            return d.GetValue(DataGridProperties.RowEditEndingCommand) as ICommand;
        }

        public static void SetRowEditEndingCommand(DependencyObject d, ICommand value)
        {
            d.SetValue(DataGridProperties.RowEditEndingCommand, value);
        }
    }
}
