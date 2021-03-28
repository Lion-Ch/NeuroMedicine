using DataLayer.Models.Classes;
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
                SendPropertyChanged(() => _probobilityDisease);
            }
        }
    }
}
