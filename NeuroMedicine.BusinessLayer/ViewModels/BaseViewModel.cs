using BusinessLayer.NotifyPropertyChanged;

namespace NeuroMedicine.BusinessLayer.ViewModels
{
    public class BaseViewModel : BaseNotifyPropertyChanged
    {        
        private string _headerVM;
        public string HeaderVM
        {
            get { return _headerVM; }
            set
            {
                _headerVM = value;
                SendPropertyChanged(() => _headerVM);
            }
        }
    }
}
