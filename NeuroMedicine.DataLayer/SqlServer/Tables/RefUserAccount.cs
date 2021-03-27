using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.SqlServer.Tables
{
    internal class RefUserAccount
    {
        [Key]
        [ForeignKey("RefUser")]
        public int Id { get; set; }
        public int Login { get; set; }
        public int Password { get; set; }

        public RefUser RefUser { get; set; }
    }
}
