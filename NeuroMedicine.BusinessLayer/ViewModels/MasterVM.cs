using BusinessLayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NeuroMedicine.BusinessLayer.ViewModels
{
    public class MasterVM : BaseViewModel
    {

        public BaseViewModel CurrentView { get; set; }


        public ICommand DiagnosticCommand { get { return new DelegateCommand(this.Diagnostic); } }
        public ICommand HistoryCommand { get { return new DelegateCommand(this.History); } }
        public ICommand PatientsCommand { get { return new DelegateCommand(this.Patients); } }
        public ICommand PersonalCabinetCommand { get { return new DelegateCommand(this.PersonalCabinet); } }
        public ICommand ExitCommand { get { return new DelegateCommand(this.Exit); } }

        private void Diagnostic(object obj)
        {
            AppContainer.Instance.ViewNavigator.NavigateToView(new DiagnosticVM());
        }
        private void History(object obj)
        {
            AppContainer.Instance.ViewNavigator.NavigateToView(new HistotyReceptionVM());
        }
        private void Patients(object obj)
        {
            AppContainer.Instance.ViewNavigator.NavigateToView(new PatientsVM());
        }
        private void PersonalCabinet(object obj)
        {
            AppContainer.Instance.ViewNavigator.NavigateToView(new PersonalCabinetVM());
        }

        private void Exit(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
