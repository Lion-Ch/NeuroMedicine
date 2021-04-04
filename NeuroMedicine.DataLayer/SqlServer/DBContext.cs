using DataLayer.SqlServer.Tables;
using System.Data.Entity;

namespace DataLayer.SqlServer
{
    internal class DBContext: DbContext
    {
        public DbSet<RefPatient> RefPatients { get; set; }
        public DbSet<RefUser> RefUsers { get; set; }
        public DbSet<RefUserAccount> RefUserAccounts { get; set; }
        public DbSet<RefPatientDiagnosis> RefPatientDiagnoses { get; set; }
        public DbSet<RefReception> RefReceptions { get; set; }

        public DBContext()
            : base("DefaultConnection")
        {
        }
    }
}
