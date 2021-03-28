using DataLayer.Models.Classes;
using DataLayer.Models.PresentationVM;
using DataLayer.SqlServer.Tables;

namespace DataLayer.Models.Factories
{
    public static class PatientFactory
    {
        public static RefPatient Create(Patient patient)
        {
            if (patient == null) return null;

            var newPatient = new RefPatient();
            newPatient.Id = patient.Id;
            newPatient.FullName = patient.FullName;
            newPatient.DateBirth = patient.DateBirth;

            return newPatient;
        }

        public static Patient Create(RefPatient patient)
        {
            if (patient == null) return null;

            var newPatient = new Patient();
            newPatient.Id = patient.Id;
            newPatient.FullName = patient.FullName;
            newPatient.DateBirth = patient.DateBirth;

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
