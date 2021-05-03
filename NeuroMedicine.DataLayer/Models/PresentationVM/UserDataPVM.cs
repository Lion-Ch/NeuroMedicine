using DataLayer.SqlServer.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.PresentationVM
{
    public class UserDataPVM
    {
        public List<RefUser> RefUsers { get; set; }
        public List<RefUserAccount> RefUserAccounts { get; set; }
        public List<RefDoctorServices> RefDoctorServices { get; set; }
        public List<RefDoctorSchedule> RefDoctorSchedules { get; set; }
    }
}
