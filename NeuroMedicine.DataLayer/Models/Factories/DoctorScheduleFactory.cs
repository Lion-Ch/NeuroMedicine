using DataLayer.Models.Classes;
using DataLayer.SqlServer.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Factories
{
    public class DoctorScheduleFactory
    {
        public static RefDoctorSchedule Create(DoctorSchedule oldObj)
        {
            if (oldObj == null) return null;

            var newOb = new RefDoctorSchedule();
            newOb.Id = oldObj.Id;
            newOb.NumDay = oldObj.NumDay;
            newOb.NumPatients = oldObj.NumPatients;
            newOb.DateEnd = oldObj.DateEnd;
            newOb.DateStart = oldObj.DateStart;

            return newOb;
        }
        public static DoctorSchedule Create(RefDoctorSchedule oldObj)
        {
            if (oldObj == null) return null;

            var newOb = new DoctorSchedule();
            newOb.Id = oldObj.Id;
            newOb.NumDay = oldObj.NumDay;
            newOb.NumPatients = oldObj.NumPatients;
            newOb.DateEnd = oldObj.DateEnd;
            newOb.DateStart = oldObj.DateStart;

            return newOb;
        }

    }
}
