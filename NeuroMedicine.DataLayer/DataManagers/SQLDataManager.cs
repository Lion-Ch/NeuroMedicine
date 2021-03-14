using DataLayer.Models.Classes;
using DataLayer.Models.Factories;
using DataLayer.SqlServer;
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
	}
}
