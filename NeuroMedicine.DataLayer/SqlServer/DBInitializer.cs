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
            db.RefUsers.Add(new SqlServer.Tables.RefUser() {Id=1 ,FullName = "Петров Петр Петрович", UserType = Models.Enums.UserType.Admin, RefUserAccount = a });
            var b = new RefUserAccount() { Login = "regis".GetHashCode(), Password = "regis".GetHashCode() };
            db.RefUsers.Add(new SqlServer.Tables.RefUser() { Id = 2, FullName = "Кириченко Людмила Петровна", UserType = Models.Enums.UserType.Registratur, RefUserAccount = b });
            var c = new RefUserAccount() { Login = "doc".GetHashCode(), Password = "doc".GetHashCode() };
            db.RefUsers.Add(new SqlServer.Tables.RefUser() { Id = 3, FullName = "Евшеев Петр Николаевич", UserType = Models.Enums.UserType.Doctor, RefUserAccount = c });

            db.RefPatients.Add(
                new RefPatient() { FullName = "Елисеев Алексей Николаевич", DateBirth = DateTime.Now.AddDays(365*-20),
                Address="Новочеркасс, улица Энгельса 25", Mail="asfasf@mail.ru", Mobile="89185530000", Gender= GenderType.Male, PassportInfo = "МВД Рос по РО 21.04.15", Passport="1234567890", Policy="1234567890123456", Snills="123456789012"});

            db.RefServices.Add(new RefService() {Id = (int)ServiceType.Consultation, Name = "Консультация", IsUseNeuralNetwork = false, Price =300, Duration=30 });
            db.RefServices.Add(new RefService() { Id = (int)ServiceType.Fluorography, Name = "Диагностика флюрографии", IsUseNeuralNetwork = true, Price = 450, Duration = 30 });
            db.RefServices.Add(new RefService() { Id = (int)ServiceType.BloodTest, Name = "Общий анализ крови", IsUseNeuralNetwork = false, Price = 270, Duration = 30 });
            db.RefServices.Add(new RefService() { Id = (int)ServiceType.ECG, Name = "ЭКГ", IsUseNeuralNetwork = false, Price = 440, Duration = 30 });
            db.RefServices.Add(new RefService() { Id = (int)ServiceType.AnalysisOfUrine, Name = "Общий анализ мочи", IsUseNeuralNetwork = false, Price = 300, Duration = 30 });


            db.RefDoctorServices.Add(new RefDoctorServices() { RefServiceId = (int)ServiceType.Consultation, RefUserId = 3 });
            db.RefDoctorServices.Add(new RefDoctorServices() { RefServiceId = (int)ServiceType.Fluorography, RefUserId = 3 });
            db.RefDoctorServices.Add(new RefDoctorServices() { RefServiceId = (int)ServiceType.BloodTest, RefUserId = 3 });
            db.RefDoctorServices.Add(new RefDoctorServices() { RefServiceId = (int)ServiceType.AnalysisOfUrine, RefUserId = 3 });

            db.RefDoctorSchedules.Add(new RefDoctorSchedule() { NumDay = 1, NumPatients = 5, TimeStart = "09:00", TimeEnd = "16:00", RefUserId = 3 });
            db.RefDoctorSchedules.Add(new RefDoctorSchedule() { NumDay = 2, NumPatients = 5, TimeStart = "09:00", TimeEnd = "16:00", RefUserId = 3 });
            db.RefDoctorSchedules.Add(new RefDoctorSchedule() { NumDay = 3, NumPatients = 5, TimeStart = "09:00", TimeEnd = "16:00", RefUserId = 3 });
            db.RefDoctorSchedules.Add(new RefDoctorSchedule() { NumDay = 0, NumPatients = 5, TimeStart = "09:00", TimeEnd = "16:00", RefUserId = 3 });

            db.RefDiagnoses.Add(new RefDiagnosis() { Name = "Без патологий", RefServiceId = 2 });
            db.RefDiagnoses.Add(new RefDiagnosis() { Name = "Пневмония",RefServiceId = 2 });
            db.RefDiagnoses.Add(new RefDiagnosis() { Name = "Рак",RefServiceId = 2 });
            db.RefDiagnoses.Add(new RefDiagnosis() { Name = "Бронхит",RefServiceId = 2 });

            db.RefSettings.Add(new RefSettings()
            {
                NameOrganization = "Медицинский центр \"Пульс\"",
                fioDirector = "Иванов Иван Иванович",
                cityOrgTag = "Новочеркасск",
                addressOrgTag = "Новочеркасск, улица Московская, д.60",
                indexOrgTag = "346234",
                phoneOrgTag = "8 800 555 35 35",
                innOrgTag = "12345678901234567890",
                rsOrgTag = "123456789012",
                bankOrgTag = "Сбербанк",
                ksOrgTag = "123456789012",
                bikOrgTag = "1234567890",
                PathToNeuro = @"C:\Users\levac\OneDrive\Рабочий стол\Учеба\Диплом\Модель"
            });

            db.SaveChanges();
        }
    }
}
