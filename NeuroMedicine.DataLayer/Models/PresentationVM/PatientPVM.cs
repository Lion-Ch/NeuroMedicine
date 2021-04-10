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
        private Service _service;
        public Service Service
        {
            get { return _service; }
            set
            {
                _service = value;
                SendPropertyChanged(() => Service);
            }
        }
        private Diagnosis _diagnosis;
        public Diagnosis Diagnosis
        {
            get { return _diagnosis; }
            set
            {
                _diagnosis = value;
                SendPropertyChanged(() => Diagnosis);
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
