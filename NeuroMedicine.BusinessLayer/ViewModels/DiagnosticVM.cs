using System.Collections.ObjectModel;
using BusinessLayer.Logic.Extensions;
using System.Windows.Input;
using BusinessLayer.Commands;
using System;
using BusinessLayer.Logic.PhotoLoader;
using DataLayer.Models.Classes;
using DataLayer.Models.PresentationVM;
using NeuroMedicine.BusinessLayer.Logic;

namespace NeuroMedicine.BusinessLayer.ViewModels
{
    public class DiagnosticVM : BaseViewModel
    {
        #region Поля класса

        private int _currentPage;
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                SendPropertyChanged(() => CurrentPage);
            }
        }

        private bool[] _isEndabledPages;
        public bool[] IsEndabledPages
        {
            get { return _isEndabledPages; }
            set
            {
                _isEndabledPages = value;
                SendPropertyChanged(() => IsEndabledPages);
            }
        }

        #endregion

        #region Поля изменения

        private ObservableCollection<ListItem> _diagnosticTypes;
        public ObservableCollection<ListItem> DiagnosticTypes
        {
            get { return _diagnosticTypes; }
            set
            {
                _diagnosticTypes = value;
                SendPropertyChanged(() => _diagnosticTypes);
            }
        }

        private int _selectedDiagnosticType;
        public int SelectedDiagnosticType
        {
            get { return _selectedDiagnosticType; }
            set { _selectedDiagnosticType = value; }
        }

        private ObservableCollection<ListItem<Patient>> _patients;
        public ObservableCollection<ListItem<Patient>> Patients
        {
            get { return _patients; }
            set
            {
                _patients = value;
                SendPropertyChanged(() => Patients);
            }
        }

        private ListItem<Patient> _selectedPatient;
        public ListItem<Patient> SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                SendPropertyChanged(() => SelectedPatient);
            }
        }
        #endregion

        #region Команды

        private DelegateCommand _proceedCommand;
        public ICommand ProceedCommand { get { return _proceedCommand; } }

        private DelegateCommand _loadPatientsFromPhotoCommand;
        public ICommand LoadPatinentFromPhotoCommand { get { return _loadPatientsFromPhotoCommand; } }

        private DelegateCommand _startAnalisisCommand;
        public ICommand StartAnalisisCommand { get { return _startAnalisisCommand; } }

        private DelegateCommand _addPatientCommand;
        public ICommand AddPatientCommand { get { return _addPatientCommand; } }

        private DelegateCommand _deletePatientCommand;
        public ICommand DeletePatientCommand { get { return _deletePatientCommand; } }

        private DelegateCommand _clearPatientListCommand;
        public ICommand ClearPatientListCommand { get { return _clearPatientListCommand; } }

        private DelegateCommand _registerNewPatientCommand;
        public ICommand RegisterNewPatientCommand { get { return _registerNewPatientCommand; } }

        private DelegateCommand _loadPhotoUrlCommand;
        public ICommand LoadPhotoUrlCommand { get { return _loadPhotoUrlCommand; } }

        private DelegateCommand _showSnapshotCommand;
        public ICommand ShowSnapshotCommand { get { return _showSnapshotCommand; } }

        private DelegateCommand _openFindPatientWindowCommand;
        public ICommand OpenFindPatientWindowCommand { get { return _openFindPatientWindowCommand; } }
        #endregion

        #region Методы команд

        private void Proceed(object parameter)
        {
            NextPage();
        }

        private void StartAnalisis(object obj)
        {
            throw new NotImplementedException();
        }

        private void AddPatient(object obj)
        {
            Patients.Add(new ListItem<Patient>());
        }

        private void DeletePatient(object obj)
        {
            if (SelectedPatient != null)
                Patients.Remove(SelectedPatient);
        }

        private void RegisterNewPatient(object obj)
        {
            throw new NotImplementedException();
        }

        private void ClearPatientList(object obj)
        {
            Patients.Clear();
        }

        private void LoadPatientsFromPhoto(object obj)
        {
            var a = new PhotoLoader().GetPathPhotos();
            foreach (var photoUrl in a)
            {
                var patient = new Patient() { PhotoUrl = photoUrl };
                Patients.Add(new ListItem<Patient> { Object = patient });
            }
        }
        private void LoadPhotoUrl(object obj)
        {
            var path = new PhotoLoader().GetPathPhoto();
            if (SelectedPatient != null && path != null)
                SelectedPatient.Object.PhotoUrl = path;
        }

        private void ShowSnapshot(object obj)
        {
            if (SelectedPatient != null && SelectedPatient.Object.PhotoUrl != null)
                AppContainer.ViewNavigator.NavigateToView(new PhotoVM(SelectedPatient.Object.PhotoUrl), true);
        }

        private void OpenFindPatientWindow(object obj)
        {
            if(SelectedPatient != null)
            {
                var findWindow = new SearchPatientVM();
                AppContainer.ViewNavigator.NavigateToView(findWindow, true);
                if (findWindow.SelectedPatient != null)
                    SelectedPatient.Object = findWindow.SelectedPatient;
            }
        }
        #endregion

        #region Публичные методы


        #endregion

        #region Приватные методы

        private void NextPage()
        {
            CurrentPage++;
            switch (_currentPage)
            {
                case 0:
                    IsEndabledPages = new bool[3] { true, false, false };
                    break;
                case 1:
                    IsEndabledPages = new bool[3] { false, true, false };
                    var a = new NeuralNetwork();
                    a.Analis();
                    break;
                case 2:
                    IsEndabledPages = new bool[3] { false, false, true };
                    break;
            }
        }

        #endregion

        public DiagnosticVM()
        {
            HeaderVM = "Диагностика пациентов";
            DiagnosticTypes = AppContainer.LocalDataManager.GetDiagnosticTypes().ToObservable();
            _proceedCommand = new DelegateCommand(this.Proceed);
            _loadPatientsFromPhotoCommand = new DelegateCommand(this.LoadPatientsFromPhoto);
            _clearPatientListCommand = new DelegateCommand(this.ClearPatientList);
            _registerNewPatientCommand = new DelegateCommand(this.RegisterNewPatient);
            _addPatientCommand = new DelegateCommand(this.AddPatient);
            _startAnalisisCommand = new DelegateCommand(this.StartAnalisis);
            _deletePatientCommand = new DelegateCommand(this.DeletePatient);
            _loadPhotoUrlCommand = new DelegateCommand(this.LoadPhotoUrl);
            _showSnapshotCommand = new DelegateCommand(this.ShowSnapshot);
            _openFindPatientWindowCommand = new DelegateCommand(this.OpenFindPatientWindow);
            _currentPage = 0;
            _isEndabledPages = new bool[3] { true, false, false };
            Patients = new ObservableCollection<ListItem<Patient>>();
        }
    }
}
