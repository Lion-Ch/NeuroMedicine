using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.SqlServer.Tables
{
    public class RefDiagnosis
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RefServiceId { get; set; }
        public RefService RefService { get; set; }
    }
}
