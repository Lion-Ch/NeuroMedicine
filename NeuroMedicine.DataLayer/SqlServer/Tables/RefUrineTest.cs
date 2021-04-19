using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.SqlServer.Tables
{
    public class RefUrineTest
    {
        public int Id { get; set; }
        public int RefPatientId { get; set; }
        public RefPatient RefPatient { get; set; }
        public int RefUserId { get; set; }
        public RefUser RefUser { get; set; }
        public DateTime Date { get; set; }

        public string Quantity { get; set; }
        public string Color { get; set; }
        public string Transparency { get; set; }
        public double SpecificGravity { get; set; }
        public string Reaction { get; set; }
        public double Protein { get; set; }
        public double Sugar { get; set; }
        public double Cylinders { get; set; }
        public double RenalEpithelium{ get; set; }
        public double Erythrocytes { get; set; }
        public double Leukocytes { get; set; }
        public double Epithelium { get; set; }
}
}
