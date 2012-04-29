using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using TaskList.Models;

namespace TaskList.DataService
{
    [ServiceContract]
    internal interface ITasksDataService
    {
        [OperationContract]
        IEnumerable<TaskModel> GetTasks();

        [OperationContract]
        bool CreateTask(TaskModel task);

        [OperationContract]
        bool DeleteTask(TaskModel task);

        [OperationContract]
        bool UpdateTask(TaskModel task);
    }

    //
    // mock of data service client
    //
    internal partial class TasksDataServiceClient : ITasksDataService
    {
        private static int count = 0;

        // thread safe id generator
        private static int GetNewId()
        {
            return Interlocked.Increment(ref TasksDataServiceClient.count);
        }

        public IEnumerable<TaskModel> GetTasks()
        {
            yield return new TaskModel { Id = TasksDataServiceClient.GetNewId(), Complete = false, Title = "read a book" };
            yield return new TaskModel { Id = TasksDataServiceClient.GetNewId(), Complete = true, Title = "write a letter" };
            yield return new TaskModel { Id = TasksDataServiceClient.GetNewId(), Complete = false, Title = "draw a doodle" };
        }

        public bool CreateTask(TaskModel task)
        {
            task.Id = TasksDataServiceClient.GetNewId();
            return true;
        }

        public bool DeleteTask(TaskModel task)
        {
            return true;
        }

        public bool UpdateTask(TaskModel task)
        {
            return true;
        }
    }
}
