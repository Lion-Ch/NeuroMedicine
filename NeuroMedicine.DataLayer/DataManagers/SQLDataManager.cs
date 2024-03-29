﻿using DataLayer.Models.Classes;
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

		public void SaveConsultation(PatientPVM patientPVM)
		{
			using (var dataContext = GetNewDataContext())
			{
				dataContext.RefConsultations.Add(PatientDiagnosisFactory.CreateConsultation(patientPVM));
				dataContext.SaveChanges();
			}
		}

        public DBContext GetNewDataContext()
		{
			var dataContext = new DBContext();
			return dataContext;
		}
		public List<RefDoctorSchedule> GetSchedules()
        {
			using (var dataContext = GetNewDataContext())
			{
				return dataContext.RefDoctorSchedules.ToList();
			}
		}
		public List<RefUser> GetUsers()
		{
			using (var dataContext = GetNewDataContext())
			{
				return dataContext.RefUsers.Include(x => x.RefUserAccount).ToList();
			}
		}
		public PatientHistory GetPatientHistory(int patientId)
        {
			PatientHistory patientHistory = new PatientHistory();
			using (var dataContext = GetNewDataContext())
			{
				patientHistory.RefBloodTests = dataContext.RefBlodTests.Where(x => x.RefPatientId == patientId)
						.OrderBy(x => x.Date).ToList();
				patientHistory.RefMeasurments = dataContext.RefMeasurments.Where(x => x.RefPatientId == patientId)
						.OrderBy(x => x.Date).ToList();
				patientHistory.RefUrineTests = dataContext.RefUrineTests.Where(x => x.RefPatientId == patientId)
						.OrderBy(x => x.Date).ToList(); 
			}
			return patientHistory;
		}
		public List<RefDoctorServices> GetAllServices()
		{
			using (var dataContext = GetNewDataContext())
			{
				return dataContext.RefDoctorServices.ToList();
			}
		}
		public void AddMeansurment(RefMeasurment means)
		{
			using (var dataContext = GetNewDataContext())
			{
				dataContext.RefMeasurments.Add(means);
				dataContext.SaveChanges();
			}
		}
		public void AddUrineTest(RefUrineTest urineTest)
		{
			using (var dataContext = GetNewDataContext())
			{
				dataContext.RefUrineTests.Add(urineTest);
				dataContext.SaveChanges();
			}
		}

		public void SaveUserData(UserDataPVM userDataPVM)
		{
			using (var db = GetNewDataContext())
			{
				foreach(var user in userDataPVM.RefUsers)
                {
					if(db.RefUsers.Where(x=>x.Id == user.Id).Count() == 0)
                    {
						db.RefUsers.Add(user);
                    }
					else
					{
						db.RefUsers.Attach(user);
						db.Entry(user).State = EntityState.Modified;
					}
                }
				foreach (var user in userDataPVM.RefUserAccounts)
				{
					if (db.RefUserAccounts.Where(x => x.Id == user.Id).Count() == 0)
					{
						db.RefUserAccounts.Add(user);
					}
					else
					{
						db.RefUserAccounts.Attach(user);
						db.Entry(user).State = EntityState.Modified;
					}
				}
				foreach (var user in userDataPVM.RefDoctorSchedules)
				{
					if (db.RefDoctorSchedules.Where(x => x.Id == user.Id).Count() == 0)
					{
						db.RefDoctorSchedules.Add(user);
					}
					else
					{
						db.RefDoctorSchedules.Attach(user);
						db.Entry(user).State = EntityState.Modified;
					}
				}
				foreach (var user in userDataPVM.RefDoctorServices)
				{
					if (db.RefDoctorServices.Where(x => x.Id == user.Id).Count() == 0)
					{
						db.RefDoctorServices.Add(user);
					}
					else
					{
						db.RefDoctorServices.Attach(user);
						db.Entry(user).State = EntityState.Modified;
					}
				}
				db.SaveChanges();
			}
		}
		public List<RefService> GetListServices()
        {
			using (var dataContext = GetNewDataContext())
			{
				return dataContext.RefServices.ToList();
			}
		}
        public Settings.Settings GetSettings()
		{
			using (var dataContext = GetNewDataContext())
			{
				return dataContext.RefSettings.ToList().Select(x => SettingsFactory.Create(x)).FirstOrDefault();
			}
		}
		public void SaveSettings(Settings.Settings settings)
		{
			using (var dataContext = GetNewDataContext())
			{
				var set = SettingsFactory.Create(settings);
				dataContext.RefSettings.Attach(set);
				dataContext.Entry(set).State = EntityState.Modified;
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
		public int GetNumberContract()
		{
			using (var context = GetNewDataContext())
			{
				return context.RefReceptions.Count() + 1;
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
		public User FindUser(string hashLogin, string hashPassword)
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
