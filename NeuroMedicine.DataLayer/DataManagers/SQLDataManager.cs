using DataLayer.Models.Classes;
using DataLayer.Models.Factories;
using DataLayer.SqlServer;
using DataLayer.SqlServer.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataManagers
{
    public class SQLDataManager
    {
		private DBContext GetNewDataContext()
		{
			var dataContext = new DBContext();
			return dataContext;
		}

		public List<Patient> FindPatients(string fullname)
        {
			using(var dataContext = GetNewDataContext())
            {
				var str = "%"+fullname+"%";

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
				var a = new RefUserAccount() { Login = "admin".GetHashCode(), Password = "admin".GetHashCode() };
				dataContext.RefUsers.Add(new SqlServer.Tables.RefUser(){ FullName = "Петров Петр Петрович", UserType = Models.Enums.UserType.Doctor, RefUserAccount = a } );
				dataContext.SaveChanges();
				var user = dataContext.RefUserAccounts
					.Where(x => x.Login == hashLogin && x.Password == hashPassword)
					.Include(x => x.RefUser)
					.ToList()
					.Select(x=>UserFactory.Create(x.RefUser))
					.FirstOrDefault();

				return user;
			}
		}

		public void AddNewDiagnosis(PatientDiagnosis diagnosis)
		{
			using (var dataContext = GetNewDataContext())
			{
				dataContext.RefPatientDiagnoses.Add(PatientDiagnosisFactory.Create(diagnosis));
				dataContext.SaveChanges();
			}
		}
	}
}
