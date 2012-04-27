using TaskList.Views;

namespace TaskList.Models
{
    internal abstract class ViewModelBase<T> where T : IView
    {
        private readonly T view;

        protected ViewModelBase(T view)
        {
            if (view != null)
            {
                view.DataContext = this;
            }
            this.view = view;
        }

        public T View { get { return this.view; } }
    }
}
