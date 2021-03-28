using Neuromedicine.Core.NotifyPropertyChanged;
using System;

namespace NeuroMedicine.BusinessLayer.ViewModels
{
    public class BaseViewModel : BaseNotifyPropertyChanged
    {
        public Action Close { get; set; }
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
