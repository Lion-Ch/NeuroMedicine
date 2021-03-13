using BusinessLayer.Logic.Factories;
using BusinessLayer.ViewModels.PresentationVM;
using System.Collections.ObjectModel;
using BusinessLayer.Logic.Extensions;
using DataLayer.Models.Enums;
using System.Windows.Input;
using BusinessLayer.Commands;

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
        #endregion
        #region Команды
        private DelegateCommand _proceedCommand;
        public ICommand ProceedCommand { get { return _proceedCommand; } }
        #endregion
        #region Методы команд
        private void Proceed(object parameter)
        {
            NextPage();
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
            DiagnosticTypes = new ListFactory().GetDiagnosticTypes().ToObservable();
            _proceedCommand = new DelegateCommand(this.Proceed);
            _currentPage = 0;
            _isEndabledPages = new bool[3] { true, false, false };
        }
    }
}
