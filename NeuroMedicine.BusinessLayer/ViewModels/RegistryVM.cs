using BusinessLayer.Commands;
using BusinessLayer.Logic.Extensions;
using DataLayer.Models.Classes;
using DataLayer.Models.Enums;
using DataLayer.Models.PresentationVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NeuroMedicine.BusinessLayer.ViewModels
{
    public class RegistryVM : BaseViewModel
    {
        private string _result;
        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                SendPropertyChanged(() => Result);
            }
        }
        private string _colorMessage;
        public string ColorMessage
        {
            get { return _colorMessage; }
            set
            {
                _colorMessage = value;
                SendPropertyChanged(() => ColorMessage);
            }
        }
        private Service _selectedDiagnosticType;
        public Service SelectedDiagnosticType
        {
            get { return _selectedDiagnosticType; }
            set 
            { 
                _selectedDiagnosticType = value;
                Doctors = AppContainer.Instance.SQLDataManager.GetDoctorByService(value.Id).ToObservable();
            }
        }
        private ObservableCollection<Service> _diagnosticTypes;
        public ObservableCollection<Service> DiagnosticTypes
        {
            get { return _diagnosticTypes; }
            set
            {
                _diagnosticTypes = value;
                SendPropertyChanged(() => _diagnosticTypes);
            }
        }
        private Service _selectedDoctor;
        public Service SelectedDoctor
        {
            get { return _selectedDoctor; }
            set
            {
                _selectedDoctor = value;
            }
        }

        private ObservableCollection<User> _doctors;
        public ObservableCollection<User> Doctors
        {
            get { return _doctors; }
            set
            {
                _doctors = value;
                SendPropertyChanged(() => Doctors);
            }
        }
        private Patient _selectedPatient;
        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                SendPropertyChanged(() => SelectedPatient);
            }
        }

        private DelegateCommand _writeSeansCommand;
        public ICommand WriteSeansCommand { get { return _writeSeansCommand; } }

        private DelegateCommand _searchPatientCommand;
        public ICommand SearchPatientCommand { get { return _searchPatientCommand; } }

        private void SearchPatient(object obj)
        {
            var findWindow = new SearchPatientVM();
            AppContainer.Instance.ViewNavigator.NavigateToModal(findWindow);
            if (findWindow.SelectedPatient != null)
                SelectedPatient = findWindow.SelectedPatient;
        }
        private void WriteSeans(object obj)
        {
            if(SelectedPatient != null)
            {
                AppContainer.Instance.SQLDataManager.WriteSeans(
                    new Reception()
                    {
                        DateRecording = DateTime.Now,
                        RefServiceId = SelectedDiagnosticType.Id,
                        RefPatientId = SelectedPatient.Id,
                        IsActive = true
                    });
                SelectedPatient = null;
                Result = "Пациент успешно записан на прием";
                ColorMessage = "Green";
            }
            else
            {
                Result = "Выберите пациента";
                ColorMessage = "Red";
            }
        }

        public RegistryVM()
        {
            HeaderVM = "Запись на прием";
            DiagnosticTypes = AppContainer.Instance.SQLDataManager.GetServices().ToObservable(); 

            _writeSeansCommand = new DelegateCommand(this.WriteSeans);
            _searchPatientCommand = new DelegateCommand(this.SearchPatient);
        }
    }
}
