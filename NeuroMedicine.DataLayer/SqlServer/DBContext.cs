using DataLayer.SqlServer.Tables;
using System.Data.Entity;

namespace DataLayer.SqlServer
{
    internal class DBContext: DbContext
    {
        public DbSet<RefPatient> RefPatients { get; set; }

        public DBContext()
            : base("DefaultConnection")
        {
        }
    }
}
