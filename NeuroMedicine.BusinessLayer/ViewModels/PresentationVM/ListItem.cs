using BusinessLayer.NotifyPropertyChanged;

namespace BusinessLayer.ViewModels.PresentationVM
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
                SendPropertyChanged(() => _name);
            }
        }
    }
}
