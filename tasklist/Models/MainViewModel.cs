using System.Collections.ObjectModel;

namespace TaskList.Models
{
    internal class MainViewModel : ModelBase
    {
        private readonly ObservableCollection<TaskModel> tasks = new ObservableCollection<TaskModel>();

        public MainViewModel()
            : base()
        {
            this.Tasks.Add(new TaskModel("read a book"));
            this.Tasks.Add(new TaskModel("write a letter"));
            this.Tasks.Add(new TaskModel("draw a doodle"));
        }

        public TaskModel SelectedItem { get; set; }

        public ObservableCollection<TaskModel> Tasks
        {
            get { return this.tasks; }
        }
    }
}
