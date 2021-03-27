using DataLayer.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Classes
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserTypeName { get; set; }
        public UserType UserType { get; set; }
    }
}
