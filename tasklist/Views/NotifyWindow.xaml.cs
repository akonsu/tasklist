using System;
using System.Drawing;
using System.Windows;
using Forms = System.Windows.Forms;

namespace TaskList.Views
{
    public partial class NotifyWindow : Window, IDisposable
    {
        private bool is_disposed = false;
        private readonly Forms.NotifyIcon notify_icon = new Forms.NotifyIcon();

        private void InitializeNotifyIcon()
        {
            var stream = Application.GetResourceStream(new Uri("pack://application:,,/Views/Resources/Main.ico")).Stream;

            this.notify_icon.Icon = new Icon(stream);
            this.notify_icon.Text = this.Title;
            this.notify_icon.Visible = true;

            this.notify_icon.Click += (s, e) =>
            {
                if (this.Visibility == Visibility.Visible)
                {
                    this.Hide();
                }
                else
                {
                    this.Show();
                }
            };
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.is_disposed)
            {
                if (disposing)
                {
                    this.notify_icon.Dispose();
                }
                this.is_disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            // Tell the garbage collector not to call the finalizer
            // since all the cleanup will already be done.
            GC.SuppressFinalize(true);
        }

        public NotifyWindow()
        {
            this.InitializeComponent();
            this.InitializeNotifyIcon();

            this.Left = SystemParameters.WorkArea.Width - this.Width - 10;
            this.Top = SystemParameters.WorkArea.Height - this.Height;
        }

        ~NotifyWindow()
        {
            this.Dispose(false);
        }
    }
}
