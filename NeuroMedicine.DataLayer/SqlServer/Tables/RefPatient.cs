using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.SqlServer.Tables
{
    internal class RefPatient
    {
        public int Id { get; set; }
        [StringLength(60)]
        public string FullName { get; set; }
        public DateTime DateBirth { get; set; }
    }
}
