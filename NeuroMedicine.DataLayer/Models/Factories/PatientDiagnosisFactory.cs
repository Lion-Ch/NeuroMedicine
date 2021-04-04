using DataLayer.Models.Classes;
using DataLayer.Models.PresentationVM;
using DataLayer.SqlServer.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Factories
{
    public static class PatientDiagnosisFactory
    {
        public static RefPatientDiagnosis Create(PatientDiagnosis oldObj)
        {
            if (oldObj == null) return null;

            var newOb = new RefPatientDiagnosis();
            newOb.Id = oldObj.Id;
            newOb.RefPatientId = oldObj.RefPatientId;
            newOb.RefUserId = oldObj.RefUserId;
            newOb.DiagnosticType = oldObj.DiagnosticType;
            newOb.Conclusion = oldObj.Conclusion;
            newOb.DiagnosysType = oldObj.DiagnosysType;
            newOb.ResultNeuralNetwork = oldObj.ResultNeuralNetwork;
            newOb.Date = oldObj.Date;
            newOb.DatePhoto = oldObj.DatePhoto;
            newOb.PhotoUrl = oldObj.PhotoUrl;
            newOb.RefReceptionId = oldObj.RefReceptionId;

            return newOb;
        }

        public static PatientDiagnosis Create(RefPatientDiagnosis oldObj)
        {
            if (oldObj == null) return null;

            var newOb = new PatientDiagnosis();
            newOb.Id = oldObj.Id;
            newOb.RefPatientId = oldObj.RefPatientId;
            newOb.RefUserId = oldObj.RefUserId;
            newOb.DiagnosticType = oldObj.DiagnosticType;
            newOb.Conclusion = oldObj.Conclusion;
            newOb.DiagnosysType = oldObj.DiagnosysType;
            newOb.ResultNeuralNetwork = oldObj.ResultNeuralNetwork;
            newOb.Date = oldObj.Date;
            newOb.RefPatient = PatientFactory.Create(oldObj.RefPatient);
            newOb.RefUser = UserFactory.Create(oldObj.RefUser);
            newOb.RefReception = ReceptionFactory.Create(oldObj.RefReception);

            return newOb;
        }
        public static PatientPVM CreatePVM(RefPatientDiagnosis oldObj)
        {
            if (oldObj == null) return null;

            var newOb = new PatientPVM();
            newOb.Id = oldObj.Id;
            newOb.DiagnosticType = oldObj.DiagnosticType;
            newOb.Conclusion = oldObj.Conclusion;
            newOb.DiagnosysType = oldObj.DiagnosysType;
            newOb.ProbobilityDisease = oldObj.ResultNeuralNetwork;
            newOb.Date = oldObj.Date;
            newOb.DatePhoto = oldObj.DatePhoto;
            newOb.PhotoUrl = oldObj.PhotoUrl;

            //newOb.RefPatientId = oldObj.Patient.Id;
            newOb.Patient = PatientFactory.Create(oldObj.RefPatient);
            //newOb.RefUserId = oldObj.User.Id;
            newOb.User = UserFactory.Create(oldObj.RefUser);

            return newOb;
        }
        public static RefPatientDiagnosis Create(PatientPVM oldObj)
        {
            if (oldObj == null) return null;

            var newOb = new RefPatientDiagnosis();
            newOb.Id = oldObj.Id;
            newOb.DiagnosticType = oldObj.DiagnosticType;
            newOb.Conclusion = oldObj.Conclusion;
            newOb.DiagnosysType = oldObj.DiagnosysType;
            newOb.ResultNeuralNetwork = oldObj.ProbobilityDisease;
            newOb.Date = oldObj.Date;
            newOb.DatePhoto = oldObj.DatePhoto;
            newOb.PhotoUrl = oldObj.PhotoUrl;

            newOb.RefPatientId = oldObj.Patient.Id;
            //newOb.RefPatient = PatientFactory.Create(oldObj.Patient);
            newOb.RefUserId = oldObj.User.Id;
            //newOb.RefUser = UserFactory.Create(oldObj.User);

            return newOb;
        }
    }
}
