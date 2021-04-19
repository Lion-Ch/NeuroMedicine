using NeuroMedicine.BusinessLayer.ViewModels;
using NeuroMedicine.BusinessLayer.ViewModels.ServicesVM;


namespace NeuroMedicine.Views.WindowView.ServicesViews
{
    /// <summary>
    /// Логика взаимодействия для UrineTestService.xaml
    /// </summary>
    public partial class UrineTestServiceView
    {
        private UrineTestServiceVM _viewModel;
        public override BaseViewModel ViewModel
        {
            get
            {
                return _viewModel;
            }
            set
            {
                _viewModel = (UrineTestServiceVM)value;
                this.DataContext = _viewModel;
            }
        }
        public UrineTestServiceView()
        {
            InitializeComponent();
        }
    }
}
