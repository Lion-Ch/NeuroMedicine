using BusinessLayer.Commands;
using BusinessLayer.Logic.Extensions;
using DataLayer.Models.PresentationVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace NeuroMedicine.BusinessLayer.ViewModels
{
    public class HistotyReceptionVM: BaseViewModel
    {
        #region Пагинация
        private int _numRecordsInPage;
        private int _totalRecords;

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

        private bool _visibleBackButton;
        public bool VisibleBackButton
        {
            get { return _visibleBackButton; }
            set
            {
                _visibleBackButton = value;
                SendPropertyChanged(() => VisibleBackButton);
            }
        }

        private bool _visibleNextButton;
        public bool VisibleNextButton
        {
            get { return _visibleNextButton; }
            set
            {
                _visibleNextButton = value;
                SendPropertyChanged(() => VisibleNextButton);
            }
        }

        private bool _visibleLastButton;
        public bool VisibleLastButton
        {
            get { return _visibleLastButton; }
            set
            {
                _visibleLastButton = value;
                SendPropertyChanged(() => VisibleLastButton);
            }
        }

        private bool _visibleFirstButton;
        public bool VisibleFirstButton
        {
            get { return _visibleFirstButton; }
            set
            {
                _visibleFirstButton = value;
                SendPropertyChanged(() => VisibleFirstButton);
            }
        }

        private int _currPage;
        public int CurrPage
        {
            get { return _currPage; }
            set
            {
                _currPage = value;
                UpdatePatients();
                UpdatePagination();
                SendPropertyChanged(() => CurrPage);
            }
        }

        private int _maxPage;
        public int MaxPage
        {
            get { return _maxPage; }
            set
            {
                _maxPage = value;
                SendPropertyChanged(() => MaxPage);
            }
        }

        private int _minPage;
        public int MinPage
        {
            get { return _minPage; }
            set
            {
                _minPage = value;
                SendPropertyChanged(() => MinPage);
            }
        }

        private DateTime _minDate;
        public DateTime MinDate
        {
            get { return _minDate; }
            set
            {
                _minDate = value;
                SendPropertyChanged(() => MinDate);
            }
        }

        private DateTime _maxDate;
        public DateTime MaxDate
        {
            get { return _maxDate; }
            set
            {
                _maxDate = value;
                SendPropertyChanged(() => MaxDate);
            }
        }
        #endregion
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

        private DelegateCommand _setDiagnosisCommand;
        public ICommand SetDiagnosisCommand { get { return _setDiagnosisCommand; } }
        private DelegateCommand _backPageCommand;
        public ICommand BackPageCommand { get { return _backPageCommand; } }
        private DelegateCommand _nextPageCommand;
        public ICommand NextPageCommand { get { return _nextPageCommand; } }
        private DelegateCommand _firstPageCommand;
        public ICommand FirstPageCommand { get { return _firstPageCommand; } }
        private DelegateCommand _lastPageCommand;
        public ICommand LastPageCommand { get { return _lastPageCommand; } }
        private DelegateCommand _searchCommand;
        public ICommand SearchCommand { get { return _searchCommand; } }

        private void SetDiagnosis(object obj)
        {
            if (SelectedPatient != null)
                AppContainer.Instance.ViewNavigator.NavigateToModal(new DiagnosisVM(SelectedPatient, true), true);
        }

        private void UpdatePagination()
        {
            if (CurrPage == MinPage)
            {
                VisibleFirstButton = false;
                VisibleBackButton = false;
            }
            else if(CurrPage == MinPage + 1)
            {
                VisibleFirstButton = false;
                VisibleBackButton = true;
            }
            else
            {
                VisibleFirstButton = true;
                VisibleBackButton = true;
            }
            if (CurrPage == MaxPage || MaxPage == 0)
            {
                VisibleLastButton = false;
                VisibleNextButton = false;
            }
            else if(CurrPage == MaxPage - 1)
            {
                VisibleLastButton = false;
                VisibleNextButton = true;
            }
            else
            {
                VisibleLastButton = true;
                VisibleNextButton = true;
            }
        }
        private void UpdatePatients()
        {
            Patients = AppContainer.Instance.SQLDataManager.GetDiagnoses(false, CurrPage, out _totalRecords, _numRecordsInPage, MinDate, MaxDate, SearchText).ToObservable();
            MaxPage = (int)Math.Ceiling((double)_totalRecords / _numRecordsInPage);
        }
        public HistotyReceptionVM()
        {
            HeaderVM = "История обследований";
            _numRecordsInPage = 20;
            MinPage = CurrPage = 1;
            MinDate = DateTime.MinValue;
            MaxDate = DateTime.MaxValue;
            UpdatePatients();
            UpdatePagination();

            _setDiagnosisCommand = new DelegateCommand(this.SetDiagnosis);
            _backPageCommand = new DelegateCommand(this.BackPage);
            _nextPageCommand = new DelegateCommand(this.NextPage);
            _firstPageCommand = new DelegateCommand(this.FirstPage);
            _lastPageCommand = new DelegateCommand(this.LastPage);
            _searchCommand = new DelegateCommand(this.Search);
        }

        private void Search(object obj)
        {
            CurrPage = 1;
            UpdatePatients();
            UpdatePagination();
        }

        private void LastPage(object obj)
        {
            CurrPage = MaxPage;
            UpdatePagination();
        }

        private void FirstPage(object obj)
        {
            CurrPage = MinPage;
            UpdatePagination();
        }

        private void NextPage(object obj)
        {
            if(CurrPage < MaxPage)
            {
                CurrPage++;
                UpdatePagination();
            }
        }

        private void BackPage(object obj)
        {
            if (CurrPage > MinPage)
            {
                CurrPage--;
                UpdatePagination();
            }
        }
    }
}
