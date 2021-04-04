using DataLayer.Models.Enums;
using Neuromedicine.Core.NotifyPropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.PresentationVM
{
    public class NewPatientPVM: BaseNotifyPropertyChanged
    {
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
        private GenderType _gender;
        public GenderType Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                SendPropertyChanged(() => Gender);
            }
        }
        private string _mobile;
        public string Mobile
        {
            get { return _mobile; }
            set
            {
                _mobile = value;
                SendPropertyChanged(() => Mobile);
            }
        }

        private string _mail;
        public string Mail
        {
            get { return _mail; }
            set
            {
                _mail = value;
                SendPropertyChanged(() => Mail);
            }
        }
        private string _address;
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                SendPropertyChanged(() => Address);
            }
        }
        private string _passport;
        public string Passport
        {
            get { return _passport; }
            set
            {
                _passport = value;
                SendPropertyChanged(() => Passport);
            }
        }
        private string _snills;
        public string Snills
        {
            get { return _snills; }
            set
            {
                _snills = value;
                SendPropertyChanged(() => Snills);
            }
        }
        private string _policy;
        public string Policy
        {
            get { return _policy; }
            set
            {
                _policy = value;
                SendPropertyChanged(() => Policy);
            }
        }
    }
}
