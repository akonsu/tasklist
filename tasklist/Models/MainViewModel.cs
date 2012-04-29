using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using TaskList.Commands;
using TaskList.DataService;

namespace TaskList.Models
{
    internal class MainViewModel : ModelBase
    {
        private TasksDataServiceClient client = new TasksDataServiceClient();

        private void OnCreateTaskCommand(object parameter)
        {
            var task = new TaskModel { Complete = false, Title = this.NewTitle };

            // create task in the database
            this.client.CreateTask(task);

            // add task to local collection
            this.Tasks.Add(task);
        }

        private void OnUpdateTaskCommand(object parameter)
        {
            if (this.SelectedTask != null)
            {
                // update current task
                this.client.UpdateTask(this.SelectedTask);
            }
        }

        private void OnTaskPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Complete")
            {
                this.CompleteTasks.View.Refresh();
                this.IncompleteTasks.View.Refresh();
            }
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
            this.CreateTaskCommand = new DelegateCommand(this.OnCreateTaskCommand);
            this.UpdateTaskCommand = new DelegateCommand(this.OnUpdateTaskCommand);

            this.CompleteTasks = new CollectionViewSource();
            this.IncompleteTasks = new CollectionViewSource();
            this.Tasks = new ObservableCollection<TaskModel>(this.client.GetTasks());

            // monitor changes to each task in collection
            foreach (INotifyPropertyChanged x in this.Tasks)
            {
                x.PropertyChanged += this.OnTaskPropertyChanged;
            }
            this.Tasks.CollectionChanged += this.OnTasksChanged;

            // split collection of tasks into two views
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
        }

        public ICommand CreateTaskCommand { get; private set; }
        public ICommand UpdateTaskCommand { get; private set; }
        public string NewTitle { get; set; }
        public TaskModel SelectedTask { get; set; }

        public CollectionViewSource CompleteTasks { get; private set; }
        public CollectionViewSource IncompleteTasks { get; private set; }
        public ObservableCollection<TaskModel> Tasks { get; private set; }
    }
}
