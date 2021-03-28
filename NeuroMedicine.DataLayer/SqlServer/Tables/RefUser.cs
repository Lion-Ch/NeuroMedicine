using DataLayer.Models.Enums;
using System;

namespace DataLayer.SqlServer.Tables
{
    public class RefUser
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public UserType UserType { get; set; }

        public RefUserAccount RefUserAccount { get; set; }
    }
}
