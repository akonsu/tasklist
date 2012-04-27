using System.Collections.ObjectModel;
using TaskList.Views;

namespace TaskList.Models
{
    internal class TaskListViewModel : ViewModelBase<IMainView>
    {
        private readonly ObservableCollection<TaskModel> tasks = new ObservableCollection<TaskModel>();

        public ObservableCollection<TaskModel> Tasks
        {
            get { return this.tasks; }
        }

        public TaskListViewModel(IMainView view)
            : base(view)
        {
            this.Tasks.Add(new TaskModel("read a book"));
            this.Tasks.Add(new TaskModel("write a letter"));
            this.Tasks.Add(new TaskModel("draw a doodle"));
        }
    }
}
