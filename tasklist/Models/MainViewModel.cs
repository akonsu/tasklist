﻿using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using LogSafe.Commands;

namespace TaskList.Models
{
    internal class MainViewModel : ModelBase
    {
        public MainViewModel()
            : base()
        {
            this.CompleteTasks = new CollectionViewSource();
            this.IncompleteTasks = new CollectionViewSource();
            this.Tasks = new ObservableCollection<TaskModel>();

            this.CompleteTasks.Source = this.Tasks;
            this.IncompleteTasks.Source = this.Tasks;

            this.CompleteTasks.Filter += (s, e) =>
            {
                var t = e.Item as TaskModel;
                e.Accepted = t != null && t.Complete;
            };

            this.IncompleteTasks.Filter += (s, e) =>
            {
                var t = e.Item as TaskModel;
                e.Accepted = t != null && !t.Complete;
            };

            this.CreateTask = new CreateTaskCommand(this);

            this.Tasks.Add(new TaskModel("read a book"));
            this.Tasks.Add(new TaskModel("write a letter"));
            this.Tasks.Add(new TaskModel("draw a doodle"));

            this.Tasks[1].Complete = true;

            this.CompleteTasks.View.Refresh();
            this.IncompleteTasks.View.Refresh();
        }

        public ICommand CreateTask { get; private set; }
        public string NewTitle { get; set; }

        public CollectionViewSource CompleteTasks { get; private set; }
        public CollectionViewSource IncompleteTasks { get; private set; }
        public ObservableCollection<TaskModel> Tasks { get; private set; }
    }
}
