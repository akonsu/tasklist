﻿using System.Windows;
using TaskList.Models;
using TaskList.Presentation;

namespace TaskList
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var m = new MainViewModel(new MainWindow());

            m.Show();
        }
    }
}
