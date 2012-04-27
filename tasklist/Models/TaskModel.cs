using System.Threading;

namespace TaskList.Models
{
    internal class TaskModel
    {
        private static int count = 0;

        // thread safe id generator
        private static int GetNewId()
        {
            return Interlocked.Increment(ref TaskModel.count);
        }

        public bool Complete { get; set; }
        public int Id { get; private set; }
        public string Title { get; private set; }

        public TaskModel(string title)
        {
            this.Complete = false;
            this.Id = TaskModel.GetNewId();
            this.Title = title;
        }
    }
}
