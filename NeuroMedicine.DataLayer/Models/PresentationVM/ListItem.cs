using Neuromedicine.Core.NotifyPropertyChanged;

namespace DataLayer.Models.PresentationVM
{
    public class ListItem: BaseNotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                SendPropertyChanged(() => Id);
            }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                SendPropertyChanged(() => Name);
            }
        }
    }
    public class ListItem<T> : BaseNotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                SendPropertyChanged(() => Id);
            }
        }
        private T _object;
        public T Object
        {
            get { return _object; }
            set
            {
                _object = value;
                SendPropertyChanged(() => Object);
            }
        }
    }
}
