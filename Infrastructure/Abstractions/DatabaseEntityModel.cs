using System.ComponentModel;

namespace Infrastructure.Abstractions
{
    public abstract class DatabaseEntityModel //: INotifyPropertyChanged
    {
        public DateTime? CreatedTime { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public DateTime? DeletedTime { get; set; }

        public bool NewAdded;
        public bool Changed;

        public event PropertyChangedEventHandler? PropertyChanged;

        public DatabaseEntityModel()
        {
            //InitializePropertyChanged();
        }

        /*protected void InitializePropertyChanged()
        {
            PropertyInfo[] properties = GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.CanRead && property.CanWrite)
                {
                    MethodInfo setMethod = property.GetSetMethod();
                    if (setMethod != null)
                    {
                        EventInfo propertyChangedEvent = typeof(INotifyPropertyChanged).GetEvent("PropertyChanged");
                        Delegate handler = Delegate.CreateDelegate(propertyChangedEvent.EventHandlerType, this, "OnPropertyChanged");

                        propertyChangedEvent.AddEventHandler(this, handler);
                    }
                }
            }
        }
        protected virtual void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(e.PropertyName));
        }*/

    }
}
