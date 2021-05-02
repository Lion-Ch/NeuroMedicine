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
    public class DiagnosisVM: BaseViewModel, ICloseWindow
    {
        //Изменяем существующую запись
        private bool _isChanged = false;

        protected PatientPVM _patientPVM;
        public PatientPVM PatientPVM
        {
            get { return _patientPVM; }
            set
            {
                _patientPVM = value;
                SendPropertyChanged(() => PatientPVM);
            }
        }

        protected ObservableCollection<Diagnosis> _diagnosysTypes;
        public ObservableCollection<Diagnosis> DiagnosysTypes
        {
            get { return _diagnosysTypes; }
            set
            {
                _diagnosysTypes = value;
                SendPropertyChanged(() => DiagnosysTypes);
            }
        }

        protected Diagnosis _selectedDiagnosysType;
        public Diagnosis SelectedDiagnosysType
        {
            get { return _selectedDiagnosysType; }
            set 
            { 
                _selectedDiagnosysType = value;
                PatientPVM.Diagnosis = value;
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

        protected virtual void Save(object obj)
        {
            if(_isChanged)
            {
                AppContainer.Instance.SQLDataManager.SaveDiagnosis(PatientPVM);
            }
            Close?.Invoke();
        }

        public DiagnosisVM(PatientPVM patientPVM, bool isChanged = false)
        {
            PatientPVM = patientPVM;
            HeaderVM = "Обследование пациента: " + patientPVM.Patient.FullName;
            _isChanged = isChanged;
            DiagnosysTypes = AppContainer.Instance.SQLDataManager.GetDiagnoses().ToObservable();
            SelectedDiagnosysType = patientPVM.Diagnosis != null ? DiagnosysTypes.Where(x =>x.Id == patientPVM.Diagnosis.Id).FirstOrDefault() : DiagnosysTypes.FirstOrDefault();
            //AppContainer.Instance.LocalDataManager.GetDagnosysTypes().ToObservable();
            _saveCommand = new DelegateCommand(this.Save);
        }

    }
}
