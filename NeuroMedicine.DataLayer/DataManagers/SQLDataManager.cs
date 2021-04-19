using DataLayer.Models.Classes;
using DataLayer.Models.Factories;
using DataLayer.Models.PresentationVM;
using DataLayer.Security;
using DataLayer.SqlServer;
using DataLayer.SqlServer.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataManagers
{
    public class SQLDataManager
    {
		private readonly int _numSaveRecord = 500;
		private DBContext GetNewDataContext()
		{
			var dataContext = new DBContext();
			return dataContext;
		}
		public void AddUrineTest(RefUrineTest urineTest)
		{
			using (var dataContext = GetNewDataContext())
			{
				dataContext.RefUrineTests.Add(urineTest);
				dataContext.SaveChanges();
			}
		}
		public void AddBloodTest(RefBloodTest bloodTest)
		{
			using (var dataContext = GetNewDataContext())
			{
				dataContext.RefBlodTests.Add(bloodTest);
				dataContext.SaveChanges();
			}
		}
		public List<Service> GetServicesByDoctor(int idDoctor)
		{
			using (var dataContext = GetNewDataContext())
			{
				var services = dataContext.RefDoctorServices
					.Where(x => x.RefUserId == idDoctor)
					.Include(x => x.RefService)
					.ToList()
					.Select(x => ServiceFactory.Create(x.RefService))
					.ToList();
				return services;
			}
		}
		/// <summary>
		/// Получает время работы врача по номеру дня
		/// </summary>
		public DoctorSchedule GetSchedule(int idDoctor, int numDay)
		{
			using (var dataContext = GetNewDataContext())
			{
				var schedule = dataContext.RefDoctorSchedules
					.Where(x => x.RefUserId == idDoctor && x.NumDay == numDay)
					.ToList()
					.Select(x=>DoctorScheduleFactory.Create(x))
					.FirstOrDefault();
				return schedule;
			}
		}
		/// <summary>
		/// Занятое время у врача
		/// </summary>
		public List<DateTime> GetNotFreeWorkTimes(int idDoctor, DateTime dateReception)
		{
			using (var dataContext = GetNewDataContext())
			{
				//Получить времена записей на дату
				var workTimes = dataContext.RefReceptions
					.Where(x => x.RefUserId == idDoctor && x.DateRecording.Year == dateReception.Year
					   && x.DateRecording.Month == dateReception.Month
					   && x.DateRecording.Day == dateReception.Day)
					.ToList()
					.Select(x=>x.DateRecording)
					.ToList();
				return workTimes;
            }
		}

		/// <summary>
		/// Дни работы врача
		/// </summary>
		public List<int> GetWorkDays(int idDoctor)
		{
			using (var dataContext = GetNewDataContext())
			{
				var days = dataContext.RefDoctorSchedules
					.Where(x => x.RefUserId == idDoctor)
					.ToList()
					.Select(x => x.NumDay)
					.ToList();
				return days;
			}
		}
		/// <summary>
		/// Ищет пациента по ФИО, который записан на прием
		/// </summary>
		public List<Patient> FindPatientsByReception(string fullname)
		{
			Encryption encryption = new Encryption();
			using (var dataContext = GetNewDataContext())
			{
				var str = "%" + fullname + "%";

				return dataContext.RefReceptions.Include(x=>x.RefPatient)
					.Where(x => DbFunctions.Like(x.RefPatient.FullName, str)
						&& x.IsActive)
					.ToList()
					.Select(x => PatientFactory.Create(x.RefPatient)).ToList();
			}
		}
		public void WriteSeans(Reception reception)
		{
			using (var context = GetNewDataContext())
			{
				context.RefReceptions.Add(ReceptionFactory.Create(reception));
				context.SaveChanges();
			}
		}
		public void AddNewPatient(NewPatientPVM patient)
		{
			using (var context = GetNewDataContext())
			{
				var pat = PatientFactory.Create(patient);
				context.RefPatients.Add(pat);
				context.SaveChanges();
			}
		}
		public void SaveDiagnosis(PatientPVM patient)
		{
			using (var context = GetNewDataContext())
			{
				var patDiag = PatientDiagnosisFactory.Create(patient);
				context.RefPatientDiagnoses.Attach(patDiag);
				context.Entry(patDiag).State = EntityState.Modified;
				context.SaveChanges();
			}
		}

		public List<PatientPVM> GetDiagnoses(bool desc, int page, out int totalRecords, int pageSize, DateTime date1, DateTime date2, string fullName = null)
		{
			IQueryable<RefPatientDiagnosis> diagnoses;
			List<PatientPVM> result = new List<PatientPVM>();

			using (var context =  GetNewDataContext())
            {
                int skipRows = (page - 1) * pageSize;

				var str = "%" + fullName + "%";
				diagnoses = context.RefPatientDiagnoses
					.Include(x => x.RefPatient).Include(x => x.RefUser).Include(x=>x.RefDiagnosis).Include(x=>x.RefService);

				if (!String.IsNullOrEmpty(fullName))
					diagnoses = diagnoses.Where(x => DbFunctions.Like(x.RefPatient.FullName, str));

				diagnoses = diagnoses.Where(x=> x.Date >= date1 && x.Date <= date2);

				totalRecords = diagnoses.Count();
				if(desc)
					result = 
						diagnoses
						.OrderBy(x=>x.Date)
						.Skip(skipRows)
						.Take(pageSize)
						.ToList()
						.Select(x=>PatientDiagnosisFactory.CreatePVM(x))
						.ToList();
				else
					result =
						diagnoses
						.OrderByDescending(x => x.Date)
						.Skip(skipRows)
						.Take(pageSize)
						.ToList()
						.Select(x => PatientDiagnosisFactory.CreatePVM(x))
						.ToList();
			}
			return result;
		}
		/// <summary>
		/// Сохраняет в базу результаты обследования
		/// </summary>
		/// <param name="patientDiagnoses">Список пациентов</param>
		public void AddPatientDiagnoses(List<PatientPVM> patientDiagnoses)
		{
            using (var dataContext = GetNewDataContext())
            {
                int i = 0;
                foreach (var pD in patientDiagnoses)
                {
                    var listReception = dataContext.RefReceptions
                        .Where(x => x.RefPatientId == pD.Patient.Id
                            && x.RefServiceId == pD.Service.Id)
                        .ToList();
                    foreach (var reception in listReception)
                    {
                        reception.IsActive = false;
                        dataContext.RefReceptions.Attach(reception);
                        dataContext.Entry(reception).State = EntityState.Modified;
                    }
                    var c = PatientDiagnosisFactory.Create(pD);
                    dataContext.RefPatientDiagnoses.Add(PatientDiagnosisFactory.Create(pD));

                    if (++i == _numSaveRecord)
                    {
                        dataContext.SaveChanges();
                        i = 0;
                    }
                }
                dataContext.SaveChanges();
            }
        }

		/// <summary>
		/// Ищет пациента по ФИО
		/// </summary>
		public List<Patient> FindPatients(string fullname)
        {
			Encryption encryption = new Encryption();
			using(var dataContext = GetNewDataContext())
            {
				var str = "%"+ fullname + "%";

				return dataContext.RefPatients
					.Where(x => DbFunctions.Like(x.FullName, str))
					.ToList()
					.Select(x => PatientFactory.Create(x)).ToList();
			}
        }
		/// <summary>
		/// Возвращает юзера по хешам логина и пароля
		/// </summary>
		public User FindUser(int hashLogin, int hashPassword)
		{
			using (var dataContext = GetNewDataContext())
			{
				var user = dataContext.RefUserAccounts
					.Where(x => x.Login == hashLogin && x.Password == hashPassword)
					.Include(x => x.RefUser)
					.ToList()
					.Select(x=>UserFactory.Create(x.RefUser))
					.FirstOrDefault();

				return user;
			}
		}
		public List<User> GetDoctorByService(int idService)
        {
			using (var dataContext = GetNewDataContext())
			{
				var services = dataContext.RefDoctorServices
					.Include(x=>x.RefUser)
					.Where(x => x.RefServiceId == idService)
					.ToList()
					.Select(x => UserFactory.Create(x.RefUser))
					.ToList();

				return services;
			}
		}
		public List<Diagnosis> GetDiagnoses()
        {
			using (var dataContext = GetNewDataContext())
			{
				var diag = dataContext.RefDiagnoses
					.ToList()
					.Select(x => DiagnosisFactory.Create(x))
					.ToList();

				return diag;
			}
		}
		/// <summary>
		/// Услуги использующие нейронную сеть
		/// </summary>
		public List<Service> GetServicesNeuro()
        {
			using (var dataContext = GetNewDataContext())
			{
				var services = dataContext.RefServices
					.Where(x=>x.IsUseNeuralNetwork)
					.ToList()
					.Select(x => ServiceFactory.Create(x))
					.ToList();

				return services;
			}
		}
		/// <summary>
		/// Возвращает список услуг
		/// </summary>
		public List<Service> GetServices()
        {
			using (var dataContext = GetNewDataContext())
			{
				var services = dataContext.RefServices.ToList().Select(x => ServiceFactory.Create(x)).ToList();

				return services;
			}
		}
		public void CheckConnect()
		{
			using (var dataContext = GetNewDataContext())
			{
			}
		}
	}
}
