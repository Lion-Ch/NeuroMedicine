using NeuroMedicine.BusinessLayer.ViewModels;
using NeuroMedicine.BusinessLayer.ViewModels.ServicesVM;

namespace NeuroMedicine.Views.WindowView.ServicesViews
{
    /// <summary>
    /// Логика взаимодействия для BloodTestView.xaml
    /// </summary>
    public partial class BloodTestView
    {
        private BloodTestServiceVM _viewModel;
        public override BaseViewModel ViewModel
        {
            get
            {
                return _viewModel;
            }
            set
            {
                _viewModel = (BloodTestServiceVM)value;
                this.DataContext = _viewModel;
            }
        }
        public BloodTestView()
        {
            InitializeComponent();
        }
    }
}
