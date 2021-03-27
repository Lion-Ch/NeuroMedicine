using NeuroMedicine.BusinessLayer.ViewModels;
using System.Windows.Controls;

namespace NeuroMedicine.Views.WindowView
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationView.xaml
    /// </summary>
    public partial class AuthorizationView
    {
        private AuthorizationVM _viewModel;
        public override BaseViewModel ViewModel
        {
            get
            {
                return _viewModel;
            }
            set
            {
                _viewModel = (AuthorizationVM)value;
                this.DataContext = _viewModel;
            }
        }
        public AuthorizationView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            var passBox = (PasswordBox)sender;
            _viewModel.Password = passBox.Password;
        }
    }
}
