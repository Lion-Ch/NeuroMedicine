using DataLayer.SqlServer.Tables;
using NeuroMedicine.BusinessLayer;
using NeuroMedicine.BusinessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NeuroMedicine.Views.WindowView
{
    /// <summary>
    /// Логика взаимодействия для SettingsView.xaml
    /// </summary>
    public partial class SettingsView
    {
        private SettingsVM _viewModel;
        public override BaseViewModel ViewModel
        {
            get
            {
                return _viewModel;
            }
            set
            {
                _viewModel = (SettingsVM)value;
                this.DataContext = _viewModel;
            }
        }
        public SettingsView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < UserGrid.SelectedItems.Count; i++)
                {
                    RefUser us = UserGrid.SelectedItems[i] as RefUser;
                    if (us != null)
                    {
                        using (var db = AppContainer.Instance.SQLDataManager.GetNewDataContext())
                        {
                            var a = db.RefUsers.Where(x => x.Id == us.Id).First();
                            db.RefUsers.Remove(a);
                            db.SaveChanges();
                        }
                    }
                }
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (LoginsGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < LoginsGrid.SelectedItems.Count; i++)
                {
                    RefUserAccount us = LoginsGrid.SelectedItems[i] as RefUserAccount;
                    if (us != null)
                    {
                        using (var db = AppContainer.Instance.SQLDataManager.GetNewDataContext())
                        {
                            var a = db.RefUserAccounts.Where(x => x.Id == us.Id).First();
                            db.RefUserAccounts.Remove(a);
                            db.SaveChanges();
                        }
                    }
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (UserServiceGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < UserServiceGrid.SelectedItems.Count; i++)
                {
                    RefDoctorServices us = UserServiceGrid.SelectedItems[i] as RefDoctorServices;
                    if (us != null)
                    {
                        using (var db = AppContainer.Instance.SQLDataManager.GetNewDataContext())
                        {
                            var a = db.RefDoctorServices.Where(x => x.Id == us.Id).First();
                            db.RefDoctorServices.Remove(a);
                            db.SaveChanges();
                        }
                    }
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (ScheduleGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < ScheduleGrid.SelectedItems.Count; i++)
                {
                    RefDoctorSchedule us = ScheduleGrid.SelectedItems[i] as RefDoctorSchedule;
                    if (us != null)
                    {
                        using (var db = AppContainer.Instance.SQLDataManager.GetNewDataContext())
                        {
                            var a = db.RefDoctorSchedules.Where(x => x.Id == us.Id).First();
                            db.RefDoctorSchedules.Remove(a);
                            db.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
