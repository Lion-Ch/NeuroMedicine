using BusinessLayer.Commands;
using BusinessLayer.Logic.Extensions;
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
    public class DiagnosisVM: BaseViewModel, ICloseWindow
    {
        private PatientPVM _patientPVM;
        public PatientPVM PatientPVM
        {
            get { return _patientPVM; }
            set
            {
                _patientPVM = value;
                SendPropertyChanged(() => _patientPVM);
            }
        }

        private ObservableCollection<ListItem> _diagnosysTypes;
        public ObservableCollection<ListItem> DiagnosysTypes
        {
            get { return _diagnosysTypes; }
            set
            {
                _diagnosysTypes = value;
                SendPropertyChanged(() => _diagnosysTypes);
            }
        }

        private int _selectedDiagnosysType;
        public int SelectedDiagnosysType
        {
            get { return _selectedDiagnosysType; }
            set 
            { 
                _selectedDiagnosysType = value;
                PatientPVM.DiagnosysType = (DiagnosysType)value;
            }
        }

        //private string _diagnosis;
        //public string Diagnosis
        //{
        //    get { return _diagnosis; }
        //    set { _diagnosis = value; }
        //}

        private DelegateCommand _saveCommand;
        public ICommand SaveCommand { get { return _saveCommand; } }

        private void Save(object obj)
        {
            PatientPVM.Date = DateTime.Now;
            Close?.Invoke();
        }

        public DiagnosisVM(PatientPVM patientPVM)
        {
            PatientPVM = patientPVM;
            HeaderVM = "Обследование пациента: " + patientPVM.Patient.FullName;
            DiagnosysTypes = AppContainer.Instance.LocalDataManager.GetDagnosysTypes().ToObservable();
            _saveCommand = new DelegateCommand(this.Save);
            SelectedDiagnosysType = (int)patientPVM.DiagnosysType;
        }

    }
}
