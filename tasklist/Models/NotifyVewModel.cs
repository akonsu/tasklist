using System.Windows.Input;

namespace TaskList.Models
{
    public class NotifyVewModel : ModelBase
    {
        public ICommand CloseCommand { get; set; }
        public ICommand ViewTasksCommand { get; set; }
    }
}
