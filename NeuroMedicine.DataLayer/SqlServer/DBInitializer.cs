using DataLayer.Models.Enums;
using DataLayer.SqlServer.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.SqlServer
{
    internal class DBInitializer: DropCreateDatabaseIfModelChanges<DBContext>
    {
        protected override void Seed(DBContext db)
        {
            var a = new RefUserAccount() { Login = "admin".GetHashCode(), Password = "admin".GetHashCode() };
            db.RefUsers.Add(new SqlServer.Tables.RefUser() {Id=1 ,FullName = "Петров Петр Петрович", UserType = Models.Enums.UserType.Doctor, RefUserAccount = a });
            db.RefPatients.Add(
                new RefPatient() { FullName = "Елисеев Алексей Николаевич", DateBirth = DateTime.Now.AddDays(365*-20)});

            db.RefServices.Add(new RefService() {Id = (int)ServiceType.Consultation, Name = "Консультация", IsUseNeuralNetwork = false });
            db.RefServices.Add(new RefService() { Id = (int)ServiceType.Fluorography, Name = "Диагностика флюрографии", IsUseNeuralNetwork = true });
            db.RefServices.Add(new RefService() { Id = (int)ServiceType.BloodTest, Name = "Общий анализ крови", IsUseNeuralNetwork = false });
            db.RefServices.Add(new RefService() { Id = (int)ServiceType.ECG, Name = "ЭКГ", IsUseNeuralNetwork = false });
            db.RefServices.Add(new RefService() { Id = (int)ServiceType.AnalysisOfUrine, Name = "Общий анализ мочи", IsUseNeuralNetwork = false });


            db.RefDoctorServices.Add(new RefDoctorServices() { RefServiceId = (int)ServiceType.Consultation, RefUserId = 1 });
            db.RefDoctorServices.Add(new RefDoctorServices() { RefServiceId = (int)ServiceType.Fluorography, RefUserId = 1 });
            db.RefDoctorServices.Add(new RefDoctorServices() { RefServiceId = (int)ServiceType.BloodTest, RefUserId = 1 });
            db.RefDoctorServices.Add(new RefDoctorServices() { RefServiceId = (int)ServiceType.AnalysisOfUrine, RefUserId = 1 });

            db.RefDoctorSchedules.Add(new RefDoctorSchedule() { NumDay = 1, NumPatients = 5, TimeStart = "09:00", TimeEnd = "16:00", RefUserId = 1 });
            db.RefDoctorSchedules.Add(new RefDoctorSchedule() { NumDay = 2, NumPatients = 5, TimeStart = "09:00", TimeEnd = "16:00", RefUserId = 1 });
            db.RefDoctorSchedules.Add(new RefDoctorSchedule() { NumDay = 3, NumPatients = 5, TimeStart = "09:00", TimeEnd = "16:00", RefUserId = 1 });
            db.RefDoctorSchedules.Add(new RefDoctorSchedule() { NumDay = 0, NumPatients = 5, TimeStart = "09:00", TimeEnd = "16:00", RefUserId = 1 });

            db.RefDiagnoses.Add(new RefDiagnosis() { Name = "Без патологий", RefServiceId = 2 });
            db.RefDiagnoses.Add(new RefDiagnosis() { Name = "Пневмония",RefServiceId = 2 });
            db.RefDiagnoses.Add(new RefDiagnosis() { Name = "Рак",RefServiceId = 2 });
            db.RefDiagnoses.Add(new RefDiagnosis() { Name = "Бронхит",RefServiceId = 2 });

            db.SaveChanges();
        }
    }
}
