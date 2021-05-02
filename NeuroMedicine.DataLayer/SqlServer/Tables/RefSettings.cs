using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.SqlServer.Tables
{
    public class RefSettings
    {
        public int Id { get; set; }
        public string NameOrganization { get; set; }
        public string fioDirector { get; set; }
        public string cityOrgTag { get; set; }
        public string addressOrgTag { get; set; }
        public string indexOrgTag { get; set; }
        public string phoneOrgTag { get; set; }
        public string innOrgTag { get; set; }
        public string rsOrgTag { get; set; }
        public string bankOrgTag { get; set; }
        public string ksOrgTag { get; set; }
        public string bikOrgTag { get; set; }
        public string PathToNeuro { get; set; }
    }
}
