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

		public void AddNewPatient(NewPatientPVM patient)
		{
			try
			{
				using (var context = GetNewDataContext())
				{
					var pat = PatientFactory.Create(patient);
					context.RefPatients.Add(pat);
					context.SaveChanges();
				}
			}
			catch (DbEntityValidationException e)
			{
				foreach (var eve in e.EntityValidationErrors)
				{
					Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
						eve.Entry.Entity.GetType().Name, eve.Entry.State);
					foreach (var ve in eve.ValidationErrors)
					{
						Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
							ve.PropertyName, ve.ErrorMessage);
					}
				}
				throw;
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
					.Include(x => x.RefPatient).Include(x => x.RefUser);

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

		public void CheckConnect()
		{
			using (var dataContext = GetNewDataContext())
			{
				var a = new RefUserAccount() { Login = "admin".GetHashCode(), Password = "admin".GetHashCode() };
				dataContext.RefUsers.Add(new SqlServer.Tables.RefUser() { FullName = "Петров Петр Петрович", UserType = Models.Enums.UserType.Doctor, RefUserAccount = a });
				dataContext.RefPatients.Add(
					new RefPatient() { FullName = "Елисеев", DateBirth = DateTime.Now });
				dataContext.SaveChanges();
			}
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
				foreach(var pD in patientDiagnoses)
				{
					dataContext.RefPatientDiagnoses.Add(PatientDiagnosisFactory.Create(pD));

					if(++i == _numSaveRecord)
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
	}
}
