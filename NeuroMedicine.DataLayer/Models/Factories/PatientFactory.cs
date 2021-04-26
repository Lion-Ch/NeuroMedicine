using DataLayer.Models.Classes;
using DataLayer.Models.PresentationVM;
using DataLayer.Security;
using DataLayer.SqlServer.Tables;

namespace DataLayer.Models.Factories
{
    public static class PatientFactory
    {
        public static RefPatient Create(NewPatientPVM patient)
        {
            if (patient == null) return null;

            Encryption encryption = new Encryption();
            var newPatient = new RefPatient();
            newPatient.FullName = patient.FullName;
            newPatient.DateBirth = patient.DateBirth;
            newPatient.Mobile = encryption.Encrypt(patient.Mobile);
            newPatient.Mail = encryption.Encrypt(patient.Mail);
            newPatient.Address = encryption.Encrypt(patient.Address);
            newPatient.Passport = encryption.Encrypt(patient.Passport);
            newPatient.Snills = encryption.Encrypt(patient.Snills);
            newPatient.Policy = encryption.Encrypt(patient.Policy);
            newPatient.Gender = patient.Gender;
            newPatient.PassportInfo = patient.PassportInfo;

            return newPatient;
        }
        public static RefPatient Create(Patient patient)
        {
            if (patient == null) return null;

            var newPatient = new RefPatient();
            newPatient.Id = patient.Id;
            newPatient.FullName = patient.FullName;
            newPatient.DateBirth = patient.DateBirth;
            newPatient.Mobile = patient.Mobile;
            newPatient.Mail = patient.Mail;
            newPatient.Address = patient.Address;
            newPatient.Passport = patient.Passport;
            newPatient.Snills = patient.Snills;
            newPatient.Policy = patient.Policy;
            newPatient.Gender = patient.Gender;
            newPatient.PassportInfo = patient.PassportInfo;

            return newPatient;
        }

        public static Patient Create(RefPatient patient)
        {
            if (patient == null) return null;

            Encryption encryption = new Encryption();
            var newPatient = new Patient();
            newPatient.Id = patient.Id;
            newPatient.FullName = patient.FullName;
            newPatient.DateBirth = patient.DateBirth;
            newPatient.Gender = patient.Gender;
            newPatient.Mobile = patient.Mobile;
            newPatient.Mail = patient.Mail;
            newPatient.Address = patient.Address;
            newPatient.Passport = patient.Passport;
            newPatient.Snills = patient.Snills;
            newPatient.Policy = patient.Policy;
            newPatient.PassportInfo = patient.PassportInfo;

            return newPatient;
        }
        public static PatientPVM CreatePVM(Patient patient)
        {
            if (patient == null) return null;

            var newPatient = new PatientPVM();
            newPatient.Patient = patient;

            return newPatient;
        }
    }
}
