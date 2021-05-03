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
                var user = AppContainer.Instance.SQLDataManager.FindUser(Login, Password);
                if (user != null)
                {
                    AppContainer.Instance.CurrentUser = user;
                    switch(user.UserType)
                    {
                        case DataLayer.Models.Enums.UserType.Admin:
                            AppContainer.Instance.ViewNavigator.NavigateToView(new SettingsVM());
                            break;
                        case DataLayer.Models.Enums.UserType.Doctor:
                            AppContainer.Instance.ViewNavigator.NavigateToView(new PersonalCabinetVM());
                            break;
                        case DataLayer.Models.Enums.UserType.Registratur:
                            AppContainer.Instance.ViewNavigator.NavigateToView(new RegistryVM());
                            break;
                    }
                }
            }
            
        }
        public AuthorizationVM()
        {
        }
    }
}
