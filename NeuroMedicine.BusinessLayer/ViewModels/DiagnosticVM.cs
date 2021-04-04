using System.Collections.ObjectModel;
using BusinessLayer.Logic.Extensions;
using System.Windows.Input;
using BusinessLayer.Commands;
using System;
using BusinessLayer.Logic.PhotoLoader;
using DataLayer.Models.Classes;
using DataLayer.Models.PresentationVM;
using NeuroMedicine.BusinessLayer.Logic;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Windows.Data;
using System.Linq;

namespace NeuroMedicine.BusinessLayer.ViewModels
{
    public class DiagnosticVM : BaseViewModel
    {
        #region Приватные поля
        private int _patientNumerator = 1;
        #endregion
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

        private bool _isReadDateFromNamePhoto;
        public bool IsReadDateFromNamePhoto
        {
            get { return _isReadDateFromNamePhoto; }
            set
            {
                _isReadDateFromNamePhoto = value;
                SendPropertyChanged(() => IsReadDateFromNamePhoto);
            }
        }

        private bool _isReadFullNameFromNamePhoto;
        public bool IsReadFullNameFromNamePhoto
        {
            get { return _isReadFullNameFromNamePhoto; }
            set
            {
                _isReadFullNameFromNamePhoto = value;
                SendPropertyChanged(() => IsReadFullNameFromNamePhoto);
            }
        }

        private bool _isUseNeuralNetwork;
        public bool IsUseNeuralNetwork
        {
            get { return _isUseNeuralNetwork; }
            set
            {
                _isUseNeuralNetwork = value;
                SendPropertyChanged(() => IsUseNeuralNetwork);
            }
        }

        private List<string> _notifications;

        public List<string> Notifications
        {
            get { return _notifications; }
            set 
            { 
                _notifications = value;
                SendPropertyChanged(() => Notifications);
            }
        }

        private List<Tuple<string, string>> _statisticNeuro;

        public List<Tuple<string, string>> StatisticNeuro
        {
            get { return _statisticNeuro; }
            set
            {
                _statisticNeuro = value;
                SendPropertyChanged(() => StatisticNeuro);
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

        private ObservableCollection<PatientPVM> _patients;
        public ObservableCollection<PatientPVM> Patients
        {
            get { return _patients; }
            set
            {
                _patients = value;
                SendPropertyChanged(() => Patients);
            }
        }

        public ICollectionView ResultPatients
        {
            get
            {
                return CollectionViewSource.GetDefaultView(Patients);
            }
        }

        private PatientPVM _selectedPatient;
        public PatientPVM SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                SendPropertyChanged(() => SelectedPatient);
            }
        }
        private byte? _minProbobility;
        public byte? MinProbobility
        {
            get { return _minProbobility; }
            set
            {
                if(_minProbobility.HasValue && value >= 0 && value <= 100)
                {
                    _minProbobility = value;
                }
                else
                    _minProbobility = 0;
                SendPropertyChanged(() => MinProbobility);
                ResultPatients.Refresh();
            }
        }
        private byte? _maxProbobility;
        public byte? MaxProbobility
        {
            get { return _maxProbobility; }
            set
            {
                if (_maxProbobility.HasValue && value >= 0 && value <= 100)
                {
                    _maxProbobility = value;
                }
                else
                    _maxProbobility = 100;
                SendPropertyChanged(() => MaxProbobility);
                ResultPatients.Refresh();
            }
        }
        #endregion

        #region Команды

        private DelegateCommand _proceedCommand;
        public ICommand ProceedCommand { get { return _proceedCommand; } }

        private DelegateCommand _loadPatientsFromPhotoCommand;
        public ICommand LoadPatinentFromPhotoCommand { get { return _loadPatientsFromPhotoCommand; } }

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

        private DelegateCommand _setDiagnosisCommand;
        public ICommand SetDiagnosisCommand { get { return _setDiagnosisCommand; } }
        #endregion

        #region Методы команд

        private void Proceed(object parameter)
        {
            switch (_currentPage)
            {
                case 0:
                        NextPage();
                    break;
                case 1:
                    {
                        CheckValidPatients();
                        if (Notifications.Count == 0)
                        {
                            StartAnalisis();
                            CalculateStatistic();
                            NextPage();
                        }
                    }
                    break;
                case 2:
                    {
                        if (Notifications.Count == 0)
                        {
                            SaveDiagnosesPatients();
                            AppContainer.Instance.ViewNavigator.NavigateToView(new PersonalCabinetVM());
                        }
                    }
                    break;
            }
        }

        private void StartAnalisis()
        {
            if(IsUseNeuralNetwork)
            {
                var neuro = new NeuralNetwork();
                foreach (var item in Patients)
                {
                    neuro.Analis(item);
                }
            }
        }

        private void AddPatient(object obj)
        {
            Patients.Add(CreatePatientPVM());
        }

        private void DeletePatient(object obj)
        {
            if (SelectedPatient != null)
                Patients.Remove(SelectedPatient);
        }

        private void RegisterNewPatient(object obj)
        {
        }

        private void ClearPatientList(object obj)
        {
            Patients.Clear();
        }

        private void LoadPatientsFromPhoto(object obj)
        {
            var a = new PhotoLoader().GetPathPhotos();
            if (a != null)
            {
                foreach (var photoUrl in a)
                {
                    var patient = CreatePatientPVM(photoUrl);
                    if (!IsReadDateFromNamePhoto)
                        patient.DatePhoto = DateTime.Now;
                    patient.Patient = AppContainer.Instance.SQLDataManager.FindPatients("ел").FirstOrDefault();
                    Patients.Add(patient);
                }
            }
        }
        private void LoadPhotoUrl(object obj)
        {
            var path = new PhotoLoader().GetPathPhoto();
            if (SelectedPatient != null && path != null)
                SelectedPatient.PhotoUrl = path;
        }

        private void ShowSnapshot(object obj)
        {
            if (SelectedPatient != null && SelectedPatient.PhotoUrl != null)
                AppContainer.Instance.ViewNavigator.NavigateToModal(new PhotoVM(SelectedPatient.PhotoUrl));
        }

        private void OpenFindPatientWindow(object obj)
        {
            if(SelectedPatient != null)
            {
                var findWindow = new SearchPatientVM();
                AppContainer.Instance.ViewNavigator.NavigateToModal(findWindow);
                if (findWindow.SelectedPatient != null)
                    SelectedPatient.Patient = findWindow.SelectedPatient;
            }
        }
        private void SetDiagnosis(object obj)
        {
            if(SelectedPatient != null)
                AppContainer.Instance.ViewNavigator.NavigateToModal(new DiagnosisVM(SelectedPatient), true);
        }

        private PatientPVM CreatePatientPVM(string photoUrl = null)
        {
            return new PatientPVM()
            {
                NumRow = _patientNumerator++,
                DatePhoto = DateTime.Now,
                DiagnosticType = (DataLayer.Models.Enums.DiagnosticType)SelectedDiagnosticType,
                Date = DateTime.Now,
                User = AppContainer.Instance.CurrentUser,
                PhotoUrl = photoUrl
            };
        }
        #endregion

        #region Публичные методы


        #endregion

        #region Приватные методы

        private void SaveDiagnosesPatients()
        {
            AppContainer.Instance.SQLDataManager.AddPatientDiagnoses(Patients.ToList());
        }
        private void CalculateStatistic()
        {
            int[] numPat = new int[4];
            foreach(var p in Patients)
            {
                if (p.ProbobilityDisease >= 85.0)
                    numPat[0]++;
                else if (p.ProbobilityDisease >= 50.0 && p.ProbobilityDisease < 85.0)
                    numPat[1]++;
                else if (p.ProbobilityDisease >= 25.0 && p.ProbobilityDisease < 50.0)
                    numPat[2]++;
                else
                    numPat[3]++;
            }

            StatisticNeuro.Add(("Вероятность", "Пациентов").ToTuple());
            StatisticNeuro.Add(("85 - 100 %", numPat[0].ToString()).ToTuple());
            StatisticNeuro.Add(("50 - 85 %", numPat[1].ToString()).ToTuple());
            StatisticNeuro.Add(("25 - 50 %", numPat[2].ToString()).ToTuple());
            StatisticNeuro.Add(("0 - 25 %", numPat[3].ToString()).ToTuple());
        }
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
                    break;
                case 2:
                    IsEndabledPages = new bool[3] { false, false, true };
                    break;
            }
        }
        private void CheckValidPatients()
        {
            List<string> err = new List<string>();
            foreach (var p in Patients)
            {
                if (p.Patient == null)
                {
                    err.Add($"Строка {p.NumRow}. Не выбран пациент!");
                }
                else
                {
                    if (String.IsNullOrEmpty(p.PhotoUrl))
                    {
                        err.Add($"Строка {p.NumRow}. Не указан путь к снимку!");
                    }
                    else if (!File.Exists(p.PhotoUrl))
                    {
                        err.Add($"Строка {p.NumRow}. Не найден снимок по данному пути!");
                    }
                    if (p.DatePhoto == null)
                    {
                        err.Add($"Строка {p.NumRow}. Не указана дата снимка!");
                    }
                    else if (p.DatePhoto.Year < 2000)
                    {
                        err.Add($"Строка {p.NumRow}. Дата снимка меньше 2000 года!");
                    }
                }
            }

            Notifications = err;
        }

        private bool FilterPartner(object parameter)
        {
            var patient = parameter as PatientPVM;

            if (patient.ProbobilityDisease >= MinProbobility && patient.ProbobilityDisease <= MaxProbobility)
                return true;

            return false;

        }
        #endregion

        public DiagnosticVM()
        {
            HeaderVM = "Диагностика пациентов";
            DiagnosticTypes = AppContainer.Instance.LocalDataManager.GetDiagnosticTypes().ToObservable();
            _proceedCommand = new DelegateCommand(this.Proceed);
            _loadPatientsFromPhotoCommand = new DelegateCommand(this.LoadPatientsFromPhoto);
            _clearPatientListCommand = new DelegateCommand(this.ClearPatientList);
            _registerNewPatientCommand = new DelegateCommand(this.RegisterNewPatient);
            _addPatientCommand = new DelegateCommand(this.AddPatient);
            _deletePatientCommand = new DelegateCommand(this.DeletePatient);
            _loadPhotoUrlCommand = new DelegateCommand(this.LoadPhotoUrl);
            _showSnapshotCommand = new DelegateCommand(this.ShowSnapshot);
            _openFindPatientWindowCommand = new DelegateCommand(this.OpenFindPatientWindow);
            _setDiagnosisCommand = new DelegateCommand(this.SetDiagnosis);
            _currentPage = 0;
            _isEndabledPages = new bool[3] { true, false, false };
            Patients = new ObservableCollection<PatientPVM>();
            Notifications = new List<string>();
            StatisticNeuro = new List<Tuple<string, string>>();
            ResultPatients.Filter = this.FilterPartner;
            MaxProbobility = 100;
            MinProbobility = 0;
            IsUseNeuralNetwork = true;
        }
    }
}
