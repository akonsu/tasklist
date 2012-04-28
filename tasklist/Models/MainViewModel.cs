using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using LogSafe.Commands;

namespace TaskList.Models
{
    internal class MainViewModel : ModelBase
    {
        private void OnTaskPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.CompleteTasks.View.Refresh();
            this.IncompleteTasks.View.Refresh();
        }

        private void OnTasksChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e != null)
            {
                if (e.OldItems != null)
                {
                    foreach (INotifyPropertyChanged x in e.OldItems)
                    {
                        x.PropertyChanged -= this.OnTaskPropertyChanged;
                    }
                }
                if (e.NewItems != null)
                {
                    foreach (INotifyPropertyChanged x in e.NewItems)
                    {
                        x.PropertyChanged += this.OnTaskPropertyChanged;
                    }
                }
            }
        }

        public MainViewModel()
            : base()
        {
            this.CompleteTasks = new CollectionViewSource();
            this.IncompleteTasks = new CollectionViewSource();
            this.Tasks = new ObservableCollection<TaskModel>();

            this.Tasks.CollectionChanged += this.OnTasksChanged;

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
        }

        public ICommand CreateTask { get; private set; }
        public string NewTitle { get; set; }

        public CollectionViewSource CompleteTasks { get; private set; }
        public CollectionViewSource IncompleteTasks { get; private set; }
        public ObservableCollection<TaskModel> Tasks { get; private set; }
    }
}
