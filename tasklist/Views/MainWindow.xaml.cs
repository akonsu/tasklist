using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using Forms = System.Windows.Forms;

namespace TaskList.Views
{
    internal enum WindowsMessage
    {
        WM_QUERYENDSESSION = 0x11,
        WM_ENDSESSION = 0x16,
        WM_SYSCOMMAND = 0x112,
    };

    public partial class MainWindow : Window
    {
        private static Forms.CloseReason close_reason = Forms.CloseReason.None;

        //
        // find the element's closest predecessor of the given type
        //
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

        private static IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch ((WindowsMessage)msg)
            {
            case WindowsMessage.WM_QUERYENDSESSION:
            case WindowsMessage.WM_ENDSESSION:
                MainWindow.close_reason = Forms.CloseReason.WindowsShutDown;
                break;

            case WindowsMessage.WM_SYSCOMMAND:
                if (((ushort)wParam & 0xfff0) == 0xf060)
                {
                    MainWindow.close_reason = Forms.CloseReason.UserClosing;
                }
                break;
            }
            return IntPtr.Zero;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (MainWindow.close_reason == Forms.CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
            MainWindow.close_reason = Forms.CloseReason.None;
        }

        //
        // this is used to enter editing mode on first click on a cell
        //
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

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            var source = PresentationSource.FromDependencyObject(this) as HwndSource;
            source.AddHook(WindowProc);
        }

        public MainWindow()
        {
            InitializeComponent();

            // commit edits when cells change focus
            this.CompleteTasks.CurrentCellChanged += (s, e) => this.CompleteTasks.CommitEdit();
            this.IncompleteTasks.CurrentCellChanged += (s, e) => this.IncompleteTasks.CommitEdit();
        }
    }
}
