using DataLayer.Models.Classes;
using DataLayer.SqlServer.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Factories
{
    public class DiagnosisFactory
    {
        public static RefDiagnosis Create(Diagnosis oldObj)
        {
            if (oldObj == null) return null;

            var newOb = new RefDiagnosis();
            newOb.Id = oldObj.Id;
            newOb.Name = oldObj.Name;

            return newOb;
        }
        public static Diagnosis Create(RefDiagnosis oldObj)
        {
            if (oldObj == null) return null;

            var newOb = new Diagnosis();
            newOb.Id = oldObj.Id;
            newOb.Name = oldObj.Name;

            return newOb;
        }
    }
}
