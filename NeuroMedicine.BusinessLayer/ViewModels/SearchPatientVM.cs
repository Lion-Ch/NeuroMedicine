using BusinessLayer.Commands;
using BusinessLayer.Logic.Extensions;
using DataLayer.Models.Classes;
using NeuroMedicine.BusinessLayer;
using NeuroMedicine.BusinessLayer.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NeuroMedicine.BusinessLayer.ViewModels
{
    public class SearchPatientVM: BaseViewModel, ICloseWindow
    {
        public Action Close { get; set; }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                SendPropertyChanged(() => SearchText);
            }
        }

        private ObservableCollection<Patient> _patients;
        public ObservableCollection<Patient> Patients
        {
            get { return _patients; }
            set
            {
                _patients = value;
                SendPropertyChanged(() => Patients);
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

        private DelegateCommand _searchCommand;
        public ICommand SearchCommand { get { return _searchCommand; } }

        private DelegateCommand _selectPatientCommand;

        public ICommand SelectPatientCommand { get { return _selectPatientCommand; } }

        private void Search(object obj)
        {
            //TODO: Сделать обращение к БД поиск по ФИО
            if(!String.IsNullOrEmpty(SearchText))
            {
                Patients = AppContainer.Instance.SQLDataManager.FindPatients(SearchText).ToObservable();
            }
        }
        private void SelectPatient(object obj)
        {
            //AppContainer.ViewNavigator.CloseView()
            Close?.Invoke();
        }

        public SearchPatientVM()
        {
            HeaderVM = "Поиск в базе";
            _searchCommand = new DelegateCommand(this.Search);
            _selectPatientCommand = new DelegateCommand(this.SelectPatient);
        }
    }
}
