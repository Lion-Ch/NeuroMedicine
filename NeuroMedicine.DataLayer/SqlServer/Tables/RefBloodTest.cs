using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.SqlServer.Tables
{
    public class RefBloodTest
    {
        public int Id { get; set; }
        public int RefPatientId { get; set; }
        public RefPatient RefPatient { get; set; }
        public int RefUserId { get; set; }
        public RefUser RefUser { get; set; }
        public DateTime Date { get; set; }


        public double ESR { get; set; }
        public double HB { get; set; }
        public double Leukocytes { get; set; }
        public double Erythrocytes { get; set; }
        public double ErythrocyteIndex { get; set; }
        public double? Coagulability { get; set; }
        public double? Platelets { get; set; }
        public double? ProthrombinIndex { get; set; }
        public double? B { get; set; }
        public double E { get; set; }
        public double? YU { get; set; }
        public double P { get; set; }
        public double FROM { get; set; }
        public double L { get; set; }
        public double M { get; set; }
    }
}
