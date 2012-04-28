using System.Threading;

namespace TaskList.Models
{
    internal class TaskModel : ModelBase
    {
        private static int count = 0;
        private bool is_complete;

        // thread safe id generator
        private static int GetNewId()
        {
            return Interlocked.Increment(ref TaskModel.count);
        }

        public bool Complete
        {
            get { return this.is_complete; }
            set
            {
                this.is_complete = value;
                base.RaisePropertyChangedEvent("Complete");
            }
        }
        public int Id { get; private set; }
        public string Title { get; set; }

        public TaskModel(string title)
        {
            this.Complete = false;
            this.Id = TaskModel.GetNewId();
            this.Title = title;
        }
    }
}
