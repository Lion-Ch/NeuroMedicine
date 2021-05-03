using DataLayer.SqlServer.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.PresentationVM
{
    public class PatientHistory
    {
        public List<RefBloodTest> RefBloodTests { get; set; }
        public List<RefUrineTest> RefUrineTests { get; set; }
        public List<RefMeasurment> RefMeasurments { get; set; }
    }
}
