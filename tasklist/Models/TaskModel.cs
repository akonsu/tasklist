using System.Runtime.Serialization;

namespace TaskList.Models
{
    [DataContract]
    public class TaskModel : ModelBase
    {
        private bool complete;
        private int? id;
        private string title;

        [DataMember]
        public bool Complete
        {
            get { return this.complete; }
            set
            {
                this.complete = value;
                base.RaisePropertyChangedEvent("Complete");
            }
        }

        [DataMember]
        public int? Id
        {
            get { return this.id; }
            set
            {
                this.id = value;
                base.RaisePropertyChangedEvent("Id");
            }
        }

        [DataMember]
        public string Title
        {
            get { return this.title; }
            set
            {
                this.title = value;
                base.RaisePropertyChangedEvent("Title");
            }
        }
    }
}
