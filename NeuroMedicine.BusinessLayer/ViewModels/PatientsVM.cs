using BusinessLayer.Commands;
using BusinessLayer.Logic.Extensions;
using DataLayer.Models.Classes;
using DataLayer.Models.PresentationVM;
using DataLayer.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NeuroMedicine.BusinessLayer.ViewModels
{
    public class PatientsVM : BaseViewModel
    {
        private string fullNameRegex = @"^[А-ЯA-Z][а-яa-zА-ЯA-Z\-]{0,}\s[А-ЯA-Z][а-яa-zА-ЯA-Z\-]{1,}(\s[А-ЯA-Z][а-яa-zА-ЯA-Z\-]{1,})?$";
        private string mobileRegex = @"^\+?(\d{1,3})?[- .]?\(?(?:\d{2,3})\)?[- .]?\d\d\d[- .]?\d\d\d\d$";
        private string emailRegex = "^[A-Z0-9._%+-]+@[A-Z0-9-]+.+.[A-Z]{2,4}$";
        private string passportRegex = @"^\d{10}$";
        private string snillsRegex = @"^\d{11}$";
        private string polyceRegex = @"^\d{16}$";


        private string _colorMessage;

        public string ColorMessage
        {
            get { return _colorMessage; }
            set
            {
                _colorMessage = value;
                SendPropertyChanged(() => ColorMessage);
            }
        }

        private string _errors;
        public string Errors
        {
            get { return _errors; }
            set
            {
                _errors = value;
                SendPropertyChanged(() => Errors);
            }
        }

        private ObservableCollection<ListItem> _genderTypes;
        public ObservableCollection<ListItem> GenderTypes
        {
            get { return _genderTypes; }
            set
            {
                _genderTypes = value;
                SendPropertyChanged(() => GenderTypes);
            }
        }

        private NewPatientPVM _patient;

        public NewPatientPVM Patient
        {
            get { return _patient; }
            set
            {
                _patient = value;
                SendPropertyChanged(() => Patient);
            }
        }

        private int _selectedGenderType;
        public int SelectedGenderType
        {
            get { return _selectedGenderType; }
            set
            {
                _selectedGenderType = value;
                Patient.Gender = (DataLayer.Models.Enums.GenderType)value;
                SendPropertyChanged(() => SelectedGenderType);
            }
        }
        private DelegateCommand _registrCommand;
        public ICommand RegistrCommand { get { return _registrCommand; } }

        private void Registr(object obj)
        {
            if (CheckPatientInfo())
            {
                Errors = "Пациент успешно добавлен в систему!";
                ColorMessage = "Green";
                Patient = new NewPatientPVM() { DateBirth = DateTime.Now };
                AppContainer.Instance.SQLDataManager.AddNewPatient(Patient);
            }
            else
            {
                ColorMessage = "Red";
            }
        }

        private bool CheckPatientInfo()
        {
            bool valid = true;
            Errors = "";
            Regex regex = new Regex(fullNameRegex, RegexOptions.IgnoreCase);
            if(String.IsNullOrEmpty(Patient.FullName))
            {
                Errors += "Не заполнено поле 'Ф.И.О.'!\n";
                valid = false;
            }
            else if (!regex.IsMatch(Patient.FullName))
            {
                Errors += "Неправильно указано Ф.И.О.!\n";
                valid = false;
            }
            if (!String.IsNullOrEmpty(Patient.Mobile))
            {
                regex = new Regex(mobileRegex, RegexOptions.IgnoreCase);
                if (!regex.IsMatch(Patient.Mobile))
                {
                    Errors += "Неправильно указан номер мобильного телефона!\n";
                    valid = false;
                }
            }
            if(Patient.DateBirth < DateTime.Parse("01.01.1921"))
            {
                Errors += "Неправильно указна дата рождения!\n";
                valid = false;
            }
            if (!String.IsNullOrEmpty(Patient.Mail))
            {
                regex = new Regex(emailRegex,RegexOptions.IgnoreCase);
                if (!regex.IsMatch(Patient.Mail))
                {
                    Errors += "Неправильно указан адрес электронной почты!\n";
                    valid = false;
                }
            }
            if (String.IsNullOrEmpty(Patient.Address))
            {
                Errors += "Не указан адрес проживания!\n";
                valid = false;
            }
            regex = new Regex(passportRegex, RegexOptions.IgnoreCase); 
            if (String.IsNullOrEmpty(Patient.Passport))
            {
                Errors += "Не заполнено поле 'Серия и номер паспорта'!\n";
                valid = false;
            }
            else if (!regex.IsMatch(Patient.Passport))
            {
                Errors += "Неправильно указаны серия и номер паспорта!\n";
                valid = false;
            }
            regex = new Regex(snillsRegex, RegexOptions.IgnoreCase);
            if (String.IsNullOrEmpty(Patient.Snills))
            {
                Errors += "Не заполнено поле 'СНИЛС'!\n";
                valid = false;
            }
            else if(!regex.IsMatch(Patient.Snills))
            {
                Errors += "Неправильно указан СНИЛС!\n";
                valid = false;
            }
            regex = new Regex(polyceRegex, RegexOptions.IgnoreCase);
            if (String.IsNullOrEmpty(Patient.Policy))
            {
                Errors += "Не заполнено поле 'Медицинский полис'!\n";
                valid = false;
            }
            else if (!regex.IsMatch(Patient.Policy))
            {
                Errors += "Неправильно указан номер медицинского полиса!\n";
                valid = false;
            }
            return valid;
        }

        public PatientsVM()
        {
            HeaderVM = "Регистрация нового пациента";
            Patient = new NewPatientPVM() { DateBirth= DateTime.Now };
            GenderTypes = AppContainer.Instance.LocalDataManager.GetGenderTypes().ToObservable();

            _registrCommand = new DelegateCommand(this.Registr);
        }
    }
}
