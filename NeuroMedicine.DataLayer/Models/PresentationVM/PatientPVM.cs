using DataLayer.Models.Classes;
using DataLayer.Models.Enums;
using Neuromedicine.Core.NotifyPropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.PresentationVM
{
    public class PatientPVM : BaseNotifyPropertyChanged
    {
        public int Id { get; set; }
        public int NumRow { get; set; }

        private Patient _patient;
        public Patient Patient
        {
            get { return _patient; }
            set
            {
                _patient = value;
                SendPropertyChanged(() => Patient);
            }
        }
        private User _user;
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                SendPropertyChanged(() => User);
            }
        }
        private DateTime _datePhoto;
        public DateTime DatePhoto
        {
            get { return _datePhoto; }
            set
            {
                _datePhoto = value;
                SendPropertyChanged(() => DatePhoto);
            }
        }
        private string _photoUrl;
        public string PhotoUrl
        {
            get { return _photoUrl; }
            set
            {
                _photoUrl = value;
                SendPropertyChanged(() => PhotoUrl);
            }
        }
        private double _probobilityDisease;
        public double ProbobilityDisease
        {
            get { return _probobilityDisease; }
            set
            {
                _probobilityDisease = value;
                SendPropertyChanged(() => ProbobilityDisease);
            }
        }
        private DiagnosticType _diagnosticType;
        public DiagnosticType DiagnosticType
        {
            get { return _diagnosticType; }
            set
            {
                _diagnosticType = value;
                SendPropertyChanged(() => DiagnosticType);
            }
        }
        private DiagnosysType _diagnosysType;
        public DiagnosysType DiagnosysType
        {
            get { return _diagnosysType; }
            set
            {
                _diagnosysType = value;
                SendPropertyChanged(() => DiagnosysType);
            }
        }
        private string _conclusion;
        public string Conclusion
        {
            get { return _conclusion; }
            set
            {
                _conclusion = value;
                SendPropertyChanged(() => Conclusion);
            }
        }
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                SendPropertyChanged(() => Date);
            }
        }
    }
}
