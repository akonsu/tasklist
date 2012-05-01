using System;
using System.Drawing;
using System.Windows;
using Forms = System.Windows.Forms;

namespace TaskList.Views
{
    public partial class NotifyWindow : Window
    {
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

        public NotifyWindow()
        {
            this.InitializeComponent();
            this.InitializeNotifyIcon();

            this.Left = SystemParameters.WorkArea.Width - this.Width - 10;
            this.Top = SystemParameters.WorkArea.Height - this.Height;
        }
    }
}
