using BusinessLayer.Commands;
using BusinessLayer.Logic.Extensions;
using DataLayer.Models.Classes;
using DataLayer.Models.Enums;
using DataLayer.Models.PresentationVM;
using NeuroMedicine.BusinessLayer.Logic.Classes;
using NeuroMedicine.BusinessLayer.ViewModels.ServicesVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NeuroMedicine.BusinessLayer.ViewModels
{
    public class PersonalCabinetVM : BaseViewModel
    {
        public string NumPatients { get; set; } = $"Принято пациентов: {AppContainer.Instance.NumPatients}";
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
        private ObservableCollection<Service> _services;
        public ObservableCollection<Service> Services
        {
            get { return _services; }
            set
            {
                _services = value;
                SendPropertyChanged(() => Services);
            }
        }
        private Service _selectedService;
        public Service SelectedService
        {
            get { return _selectedService; }
            set
            {
                _selectedService = value;
                SendPropertyChanged(() => SelectedService);
            }
        }
        public ICommand SearchPatientCommand { get { return new DelegateCommand(this.SearchPatient); } }

        private void SearchPatient(object obj)
        {
            var findWindow = new SearchPatientVM();
            AppContainer.Instance.ViewNavigator.NavigateToModal(findWindow);
            if (findWindow.SelectedPatient != null)
                SelectedPatient = findWindow.SelectedPatient;
        }
        public ICommand SelectServiceCommand { get { return new DelegateCommand(this.SelectService); } }

        private void SelectService(object obj)
        {
            if(SelectedPatient != null && SelectedService != null)
            {
                //Переход на выбранную услугу
                switch (SelectedService.Id)
                {
                    case (int)ServiceType.Consultation:
                        break;
                    case (int)ServiceType.BloodTest:
                        AppContainer.Instance.ViewNavigator.NavigateToView(new BloodTestServiceVM(SelectedPatient));
                        break;
                    case (int)ServiceType.ECG:
                        break;
                    case (int)ServiceType.AnalysisOfUrine:
                        AppContainer.Instance.ViewNavigator.NavigateToView(new UrineTestServiceVM(SelectedPatient));
                        break;
                }
            }
        }

        public PersonalCabinetVM()
        {
            HeaderVM = "Личный кабинет";
            Services = AppContainer.Instance.SQLDataManager.GetServicesByDoctor(AppContainer.Instance.CurrentUser.Id).ToObservable();
        }
    }
}
