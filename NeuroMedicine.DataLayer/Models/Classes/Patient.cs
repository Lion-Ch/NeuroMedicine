using Neuromedicine.Core.NotifyPropertyChanged;
using System;

namespace DataLayer.Models.Classes
{
    public class Patient: BaseNotifyPropertyChanged
    {
        public int Id { get; set; }

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                SendPropertyChanged(() => FullName);
            }
        }
        private DateTime _dateBirth;
        public DateTime DateBirth
        {
            get { return _dateBirth; }
            set
            {
                _dateBirth = value;
                SendPropertyChanged(() => DateBirth);
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
