using System.ComponentModel;

namespace TaskList.Models
{
    internal abstract class ModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent(string name)
        {
            if (this.PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs(name);
                this.PropertyChanged(this, e);
            }
        }
    }
}
