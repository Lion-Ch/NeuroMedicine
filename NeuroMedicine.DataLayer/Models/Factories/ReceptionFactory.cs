using DataLayer.Models.Classes;
using DataLayer.SqlServer.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Factories
{
    public static class ReceptionFactory
    {
        public static RefReception Create(Reception oldObj)
        {
            if (oldObj == null) return null;

            var newOb = new RefReception();
            newOb.Id = oldObj.Id;
            newOb.RefPatientId = oldObj.RefPatientId;
            newOb.DateRecording = oldObj.DateRecording;
            newOb.IsActive = oldObj.IsActive;
            newOb.RefServiceId = oldObj.RefServiceId;
            newOb.RefUserId = oldObj.RefUserId;
            //newOb.DiagnosticType = oldObj.DiagnosticType;

            return newOb;
        }
        public static Reception Create(RefReception oldObj)
        {
            if (oldObj == null) return null;

            var newOb = new Reception();
            newOb.Id = oldObj.Id;
            newOb.RefPatient = PatientFactory.Create(oldObj.RefPatient);
            newOb.IsActive = oldObj.IsActive;
            newOb.RefPatientId = oldObj.RefPatientId;
            newOb.DateRecording = oldObj.DateRecording;
            newOb.RefUserId = oldObj.RefUserId;
            //newOb.DiagnosticType = oldObj.DiagnosticType;

            return newOb;
        }
    }
}
