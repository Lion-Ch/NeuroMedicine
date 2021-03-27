using BusinessLayer.Commands;
using System;
using System.Windows.Input;

namespace NeuroMedicine.BusinessLayer.ViewModels
{
    public class AuthorizationVM : BaseViewModel
    {
        private string _login;
        public string Login
        {
            get { return _login; }
            set { _login = value; SendPropertyChanged(() => Login); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; SendPropertyChanged(() => Password); }
        }

        public ICommand LogInCommand { get { return new DelegateCommand(this.LogIn); } }

        private void LogIn(object parameter)
        {
            if(!(String.IsNullOrEmpty(Login)&& String.IsNullOrEmpty(Password)))
            {
                var user = AppContainer.Instance.SQLDataManager.FindUser(Login.GetHashCode(), Password.GetHashCode());
                if (user != null)
                {
                    AppContainer.Instance.CurrentUser = user;
                    AppContainer.Instance.ViewNavigator.NavigateToView(new HistotyReceptionVM());
                }
            }
            
        }
        public AuthorizationVM()
        {

        }
    }
}
