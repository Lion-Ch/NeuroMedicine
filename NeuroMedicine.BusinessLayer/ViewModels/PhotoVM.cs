

namespace NeuroMedicine.BusinessLayer.ViewModels
{
    public class PhotoVM : BaseViewModel
    {
        private string _photoUrl;
        public string PhotoUrl
        {
            get { return _photoUrl; }
            set
            {
                _photoUrl = value;
                SendPropertyChanged(() => PhotoUrl);
            }
        }
        public PhotoVM(string photoUrl)
        {
            HeaderVM = "Снимок пациента";
            PhotoUrl = photoUrl;
        }
    }
}
