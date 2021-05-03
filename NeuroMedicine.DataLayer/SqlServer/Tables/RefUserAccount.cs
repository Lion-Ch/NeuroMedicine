using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.SqlServer.Tables
{
    public class RefUserAccount
    {
        [Key]
        [ForeignKey("RefUser")]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public RefUser RefUser { get; set; }
    }
}
