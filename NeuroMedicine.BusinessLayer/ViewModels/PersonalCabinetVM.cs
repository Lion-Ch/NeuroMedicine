using BusinessLayer.Commands;
using BusinessLayer.Logic.Extensions;
using DataLayer.Models.Classes;
using DataLayer.Models.Enums;
using DataLayer.Models.PresentationVM;
using DataLayer.SqlServer.Tables;
using NeuroMedicine.BusinessLayer.Logic.Classes;
using NeuroMedicine.BusinessLayer.ViewModels.ServicesVM;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
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
        public User User { get; set; }
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
            {
                SelectedPatient = findWindow.SelectedPatient;
                LoadGraphics();
            }
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
                        AppContainer.Instance.ViewNavigator.NavigateToView(new ConsultationVM(new PatientPVM()
                        {
                            Patient = SelectedPatient,
                            Service = SelectedService,
                            User = AppContainer.Instance.CurrentUser
                        }));
                        break;
                    case (int)ServiceType.BloodTest:
                        AppContainer.Instance.ViewNavigator.NavigateToView(new BloodTestServiceVM(SelectedPatient));
                        break;
                    case (int)ServiceType.ECG:
                        break;
                    case (int)ServiceType.AnalysisOfUrine:
                        AppContainer.Instance.ViewNavigator.NavigateToView(new UrineTestServiceVM(SelectedPatient));
                        break;
                    case (int)ServiceType.Meansurment:
                        AppContainer.Instance.ViewNavigator.NavigateToView(new MeansurmentVM(SelectedPatient));
                        break;
                }
            }
        }

        public PersonalCabinetVM()
        {
            HeaderVM = "Личный кабинет";
            Services = AppContainer.Instance.SQLDataManager.GetServicesByDoctor(AppContainer.Instance.CurrentUser.Id).ToObservable();
            User = AppContainer.Instance.CurrentUser;
        }

        private void LoadGraphics()
        {
            var history = AppContainer.Instance.SQLDataManager.GetPatientHistory(SelectedPatient.Id);
            UdelWeight = new List<DataPoint>();
            Height = new List<DataPoint>();
            Weight = new List<DataPoint>();
            Pulse = new List<DataPoint>();
            SistPrepare = new List<DataPoint>();
            DiastPrepare = new List<DataPoint>();
            SOE = new List<DataPoint>();
            Hb = new List<DataPoint>();
            Lekocit = new List<DataPoint>();
            Eritrocit = new List<DataPoint>();
            EritrocitIndex = new List<DataPoint>();
            B = new List<DataPoint>();
            E = new List<DataPoint>();
            YU = new List<DataPoint>();
            P = new List<DataPoint>();
            S = new List<DataPoint>();
            L = new List<DataPoint>();
            M = new List<DataPoint>();
            foreach (var i in history.RefUrineTests)
            {
                UdelWeight.Add(new DataPoint(DateTimeAxis.ToDouble(i.Date), i.SpecificGravity));
            }
            foreach (var i in history.RefMeasurments)
            {
                if (i.Height.HasValue)
                    Height.Add(new DataPoint(DateTimeAxis.ToDouble(i.Date), i.Height.Value));
                if (i.Weight.HasValue)
                    Weight.Add(new DataPoint(DateTimeAxis.ToDouble(i.Date), i.Weight.Value));
                if (i.Pulse.HasValue)
                    Pulse.Add(new DataPoint(DateTimeAxis.ToDouble(i.Date), i.Pulse.Value));
                if (i.PressureDiastolic.HasValue)
                    DiastPrepare.Add(new DataPoint(DateTimeAxis.ToDouble(i.Date), i.PressureDiastolic.Value));
                if (i.PressureSystolic.HasValue)
                    SistPrepare.Add(new DataPoint(DateTimeAxis.ToDouble(i.Date), i.PressureSystolic.Value));
            }
            foreach (var i in history.RefBloodTests)
            {
                SOE.Add(new DataPoint(DateTimeAxis.ToDouble(i.Date), i.ESR));
                Hb.Add(new DataPoint(DateTimeAxis.ToDouble(i.Date), i.HB));
                Lekocit.Add(new DataPoint(DateTimeAxis.ToDouble(i.Date), i.Leukocytes));
                Eritrocit.Add(new DataPoint(DateTimeAxis.ToDouble(i.Date), i.Erythrocytes));
                EritrocitIndex.Add(new DataPoint(DateTimeAxis.ToDouble(i.Date), i.ErythrocyteIndex));
                if(i.B.HasValue)
                    B.Add(new DataPoint(DateTimeAxis.ToDouble(i.Date), i.B.Value));
                E.Add(new DataPoint(DateTimeAxis.ToDouble(i.Date), i.E));
                if (i.YU.HasValue)
                    YU.Add(new DataPoint(DateTimeAxis.ToDouble(i.Date), i.YU.Value));
                P.Add(new DataPoint(DateTimeAxis.ToDouble(i.Date), i.P));
                S.Add(new DataPoint(DateTimeAxis.ToDouble(i.Date), i.FROM));
                L.Add(new DataPoint(DateTimeAxis.ToDouble(i.Date), i.L));
                M.Add(new DataPoint(DateTimeAxis.ToDouble(i.Date), i.M));
            }
            SendPropertyChanged(() =>UdelWeight);
            SendPropertyChanged(() =>Height);
            SendPropertyChanged(() =>Weight);
            SendPropertyChanged(() =>Pulse);
            SendPropertyChanged(() =>SistPrepare);
            SendPropertyChanged(() =>DiastPrepare);
            SendPropertyChanged(() =>SOE);
            SendPropertyChanged(() => Hb);
            SendPropertyChanged(() => Lekocit);
            SendPropertyChanged(() => Eritrocit);
            SendPropertyChanged(() => EritrocitIndex);
            SendPropertyChanged(() => B);
            SendPropertyChanged(() => E);
            SendPropertyChanged(() => YU);
            SendPropertyChanged(() => P);
            SendPropertyChanged(() => S);
            SendPropertyChanged(() => L);
            SendPropertyChanged(() => M);
        }

        private List<DataPoint> _height;
        public List<DataPoint> Height
        {
            get { return _height; }
            set
            {
                _height = value;
                SendPropertyChanged(() => Height);
            }
        }
        private List<DataPoint> _weight;
        public List<DataPoint> Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                SendPropertyChanged(() => Weight);
            }
        }
        private List<DataPoint> _pulse;
        public List<DataPoint> Pulse
        {
            get { return _pulse; }
            set
            {
                _pulse = value;
                SendPropertyChanged(() => Pulse);
            }
        }
        private List<DataPoint> _sistPrepare;
        public List<DataPoint> SistPrepare
        {
            get { return _sistPrepare; }
            set
            {
                _sistPrepare = value;
                SendPropertyChanged(() => SistPrepare);
            }
        }
        private List<DataPoint> _diastPrepare;
        public List<DataPoint> DiastPrepare
        {
            get { return _diastPrepare; }
            set
            {
                _diastPrepare = value;
                SendPropertyChanged(() => DiastPrepare);
            }
        }
        private List<DataPoint> _SOE;
        public List<DataPoint> SOE
        {
            get { return _SOE; }
            set
            {
                _SOE = value;
                SendPropertyChanged(() => SOE);
            }
        }
        private List<DataPoint> _hb;
        public List<DataPoint> Hb
        {
            get { return _hb; }
            set
            {
                _hb = value;
                SendPropertyChanged(() => Hb);
            }
        }
        private List<DataPoint> _lekocit;
        public List<DataPoint> Lekocit
        {
            get { return _lekocit; }
            set
            {
                _lekocit = value;
                SendPropertyChanged(() => Lekocit);
            }
        }
        private List<DataPoint> _eritrocit;
        public List<DataPoint> Eritrocit
        {
            get { return _eritrocit; }
            set
            {
                _eritrocit = value;
                SendPropertyChanged(() => Eritrocit);
            }
        }
        private List<DataPoint> _eritrocitIndex;
        public List<DataPoint> EritrocitIndex
        {
            get { return _eritrocitIndex; }
            set
            {
                _eritrocitIndex = value;
                SendPropertyChanged(() => EritrocitIndex);
            }
        }
        private List<DataPoint> _b;
        public List<DataPoint> B
        {
            get { return _b; }
            set
            {
                _b = value;
                SendPropertyChanged(() => B);
            }
        }
        private List<DataPoint> _e;
        public List<DataPoint> E
        {
            get { return _e; }
            set
            {
                _e = value;
                SendPropertyChanged(() => E);
            }
        }
        private List<DataPoint> _YU;
        public List<DataPoint> YU
        {
            get { return _YU; }
            set
            {
                _YU = value;
                SendPropertyChanged(() => YU);
            }
        }
        private List<DataPoint> _p;
        public List<DataPoint> P
        {
            get { return _p; }
            set
            {
                _p = value;
                SendPropertyChanged(() => P);
            }
        }
        private List<DataPoint> _s;
        public List<DataPoint> S
        {
            get { return _s; }
            set
            {
                _s = value;
                SendPropertyChanged(() => S);
            }
        }
        private List<DataPoint> _l;
        public List<DataPoint> L
        {
            get { return _l; }
            set
            {
                _l = value;
                SendPropertyChanged(() => L);
            }
        }
        private List<DataPoint> _m;
        public List<DataPoint> M
        {
            get { return _m; }
            set
            {
                _m = value;
                SendPropertyChanged(() => M);
            }
        }
        private List<DataPoint> _udelWeight;
        public List<DataPoint> UdelWeight
        {
            get { return _udelWeight; }
            set
            {
                _udelWeight = value;
                SendPropertyChanged(() => UdelWeight);
            }
        }
    }
}
