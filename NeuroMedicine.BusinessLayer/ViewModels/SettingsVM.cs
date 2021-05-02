using BusinessLayer.Commands;
using DataLayer.Settings;
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

        public ICommand SaveSettingsCommand
        {
            get { return new DelegateCommand(this.SaveSettings); }
        }
        public ICommand ShowPathCommand
        {
            get { return new DelegateCommand(this.ShowPath); }
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
        }
    }
}
