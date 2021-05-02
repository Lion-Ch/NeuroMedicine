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
using System.Windows.Shapes;

namespace NeuroMedicine.Views.WindowView
{
    /// <summary>
    /// Логика взаимодействия для MasterWindowView.xaml
    /// </summary>
    public partial class MasterWindowView
    {
        public MasterWindowView()
        {
            InitializeComponent();
        }

        private void Diagnostic_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BussinesModel.ViewNavigator.NavigateToView(new DiagnosticVM());
        }
        private void History_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BussinesModel.ViewNavigator.NavigateToView(new HistotyReceptionVM());
        }
        private void Patients_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BussinesModel.ViewNavigator.NavigateToView(new PatientsVM());
        }
        private void PersonalCabinet_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BussinesModel.ViewNavigator.NavigateToView(new PersonalCabinetVM());
        }
        private void Exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BussinesModel.ViewNavigator.NavigateToView(new AuthorizationVM(), false);
        }
        private void Settings_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void Registratur_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BussinesModel.ViewNavigator.NavigateToView(new RegistryVM());
        }
    }
}
