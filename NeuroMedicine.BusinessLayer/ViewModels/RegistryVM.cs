using BusinessLayer.Commands;
using BusinessLayer.Logic.Extensions;
using DataLayer.Models.Classes;
using DataLayer.Models.Enums;
using DataLayer.Models.PresentationVM;
using NeuroMedicine.BusinessLayer.Logic.Classes;
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
                if (value != null)
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
        private User _selectedDoctor;
        public User SelectedDoctor
        {
            get { return _selectedDoctor; }
            set
            {
                _selectedDoctor = value;
                UpdateDaysWork();
            }
        }
        private DateTime? _selectedDayWork;
        public DateTime? SelectedDayWork
        {
            get { return _selectedDayWork; }
            set
            {
                _selectedDayWork = value;
                UpdateTimeWork();
            }
        }
        private DateTime? _selectedTime;
        public DateTime? SelectedTime
        {
            get { return _selectedTime; }
            set
            {
                _selectedTime = value;
            }
        }
        public ObservableCollection<DateTime> _times;
        public ObservableCollection<DateTime> Times
        {
            get { return _times; }
            set
            {
                _times = value;
                SendPropertyChanged(() => Times);
            }
        }

        public ObservableCollection<DateTime> _daysWork;
        public ObservableCollection<DateTime> DaysWork
        {
            get { return _daysWork; }
            set
            {
                _daysWork = value;
                SendPropertyChanged(() => DaysWork);
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
            if (SelectedPatient == null)
            {
                Result = "Выберите пациента";
                ColorMessage = "Red";
            }
            else if (SelectedDiagnosticType == null)
            {
                Result = "Выберите услугу";
                ColorMessage = "Red";
            }
            else if (SelectedDoctor == null)
            {
                Result = "Выберите доктора";
                ColorMessage = "Red";
            }
            else if (SelectedTime == null)
            {
                Result = "Выберите время приема";
                ColorMessage = "Red";
            }
            else if (SelectedDayWork == null)
            {
                Result = "Выберите дату приема";
                ColorMessage = "Red";
            }
            else
            {
                AppContainer.Instance.SQLDataManager.WriteSeans(
                    new Reception()
                    {
                        DateRecording = new DateTime(SelectedDayWork.Value.Year, SelectedDayWork.Value.Month, SelectedDayWork.Value.Day,
                            SelectedTime.Value.Hour, SelectedTime.Value.Minute, SelectedTime.Value.Second),
                        RefServiceId = SelectedDiagnosticType.Id,
                        RefPatientId = SelectedPatient.Id,
                        RefUserId = SelectedDoctor.Id,
                        IsActive = true
                    });
                DiagnosticTypes = _diagnosticTypes;
                SelectedPatient = null;
                SelectedDiagnosticType = null;
                //Doctors = null;
                Times = null;
                DaysWork = null;
                Result = "Пациент успешно записан на прием";
                ColorMessage = "Green";
            }
        }


        private void UpdateDaysWork()
        {
            if(SelectedDoctor != null)
            {
                DateTimeConverter daysConverter = new DateTimeConverter(AppContainer.Instance.SQLDataManager.GetWorkDays(SelectedDoctor.Id));
                DaysWork = daysConverter.GetWorkDays().ToObservable();
            }
        }
        private void UpdateTimeWork()
        {
            if (SelectedDayWork != null)
            {
                var doctorSchedule = AppContainer.Instance.SQLDataManager.GetSchedule(AppContainer.Instance.CurrentUser.Id,
                    (int)SelectedDayWork.Value.DayOfWeek);
                var times = new DateTimeConverter().GetFreeWorkTimes(DateTime.Parse(doctorSchedule.TimeStart), DateTime.Parse(doctorSchedule.TimeEnd), doctorSchedule.NumPatients, AppContainer.Instance.SQLDataManager.GetNotFreeWorkTimes(AppContainer.Instance.CurrentUser.Id, SelectedDayWork.Value)).ToObservable();
                Times = times;
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
