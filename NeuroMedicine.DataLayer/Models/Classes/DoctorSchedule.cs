using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Classes
{
    public class DoctorSchedule
    {
        public int Id { get; set; }
        public int NumDay { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public int NumPatients { get; set; }
    }
}
