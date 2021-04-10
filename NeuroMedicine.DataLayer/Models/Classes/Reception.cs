using DataLayer.Models.Enums;
using DataLayer.SqlServer.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Classes
{
    public class Reception
    {
        public int Id { get; set; }
        public int RefPatientId { get; set; }
        public Patient RefPatient { get; set; }
        public bool IsActive { get; set; }
        public int RefServiceId { get; set; }
        public Service RefService { get; set; }
        public DateTime DateRecording { get; set; }
    }
}
