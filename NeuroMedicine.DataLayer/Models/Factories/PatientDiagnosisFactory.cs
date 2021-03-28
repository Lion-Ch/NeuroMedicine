using DataLayer.Models.Classes;
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

            return newOb;
        }
    }
}
