using TaskList.Views;

namespace TaskList.Models
{
    internal class MainViewModel : ViewModelBase<IMainView>
    {
        private readonly TaskListViewModel task_list;

        public MainViewModel(IMainView view)
            : base(view)
        {
            this.task_list = new TaskListViewModel(view);
        }

        public void Show()
        {
            this.View.Show();
        }
    }
}
