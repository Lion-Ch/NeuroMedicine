using BusinessLayer.Commands;
using DataLayer.Models.PresentationVM;
using DataLayer.Settings;
using DataLayer.SqlServer.Tables;
//using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WinForms = System.Windows.Forms;

namespace NeuroMedicine.BusinessLayer.ViewModels
{
    public class SettingsVM : BaseViewModel
    {
        private Settings _settings;
        public Settings Settings
        {
            get { return _settings; }
            set
            {
                _settings = value;
                SendPropertyChanged(()=>Settings);
            }
        }

        private List<RefUser> _users;
        public List<RefUser> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                SendPropertyChanged(() => Users);
            }
        }
        private List<RefUserAccount> _logins;
        public List<RefUserAccount> Logins
        {
            get { return _logins; }
            set
            {
                _logins = value;
                SendPropertyChanged(() => Logins);
            }
        }
        private List<RefDoctorSchedule> _schedules;
        public List<RefDoctorSchedule> Schedules
        {
            get { return _schedules; }
            set
            {
                _schedules = value;
                SendPropertyChanged(() => Schedules);
            }
        }
        public List<RefService> AllServices { get; set; }
        private List<RefDoctorServices> _userServices;
        public List<RefDoctorServices> UserServices
        {
            get { return _userServices; }
            set
            {
                _userServices = value;
                SendPropertyChanged(() => UserServices);
            }
        }
        public ICommand SaveSettingsCommand
        {
            get { return new DelegateCommand(this.SaveSettings); }
        }
        public ICommand ShowPathCommand
        {
            get { return new DelegateCommand(this.ShowPath); }
        }
        public ICommand SaveUserDataCommand
        {
            get { return new DelegateCommand(this.SaveUserData); }
        }
        public ICommand UpdateCommand
        {
            get { return new DelegateCommand(this.Update); }
        }

        private void Update(object obj)
        {
            LoadAccounts();
        }

        private void SaveUserData(object obj)
        {
            try
            {
                AppContainer.Instance.SQLDataManager.SaveUserData(new UserDataPVM
                {
                    RefUsers = Users,
                    RefDoctorSchedules = Schedules,
                    RefDoctorServices = UserServices,
                    RefUserAccounts = Logins
                });
            }
            catch(Exception e)
            {
                AppContainer.Instance.ViewNavigator.NavigateToModal(new ConfirmationModalVM(e.Message));
            }
        }

        private void ShowPath(object obj)
        {
            WinForms.FolderBrowserDialog folderBrowserDialog = new WinForms.FolderBrowserDialog();

            // если папка выбрана и нажата клавиша `OK` - значит можно получить путь к папке
            if (folderBrowserDialog.ShowDialog() == WinForms.DialogResult.OK)
            {
                Settings.PathToNeuro = folderBrowserDialog.SelectedPath;
                SendPropertyChanged(() => Settings);
            }
        }

        private void SaveSettings(object obj)
        {
            AppContainer.Instance.SQLDataManager.SaveSettings(Settings);
            AppContainer.Instance.ViewNavigator.NavigateToModal(new ConfirmationModalVM("Настройки сохранены"));
        }

        public SettingsVM()
        {
            HeaderVM = "Настройки";
            Settings = AppContainer.Instance.SQLDataManager.GetSettings();
            LoadAccounts();
        }
        private void LoadAccounts()
        {
            Users = AppContainer.Instance.SQLDataManager.GetUsers();
            Schedules = AppContainer.Instance.SQLDataManager.GetSchedules();
            UserServices = AppContainer.Instance.SQLDataManager.GetAllServices();
            List<RefUserAccount> userAccounts = new List<RefUserAccount>();
            foreach(var i in Users)
            {
                userAccounts.Add(i.RefUserAccount);
            }
            Logins = userAccounts;
            AllServices = AppContainer.Instance.SQLDataManager.GetListServices();
        }
    }
}
