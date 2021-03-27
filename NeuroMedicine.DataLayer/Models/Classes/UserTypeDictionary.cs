using DataLayer.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Classes
{
    internal class UserTypeDictionary
    {
        private static Dictionary<UserType, string> Dictionary = new Dictionary<UserType, string>()
        {
            {UserType.Admin, "Администратор" },
            {UserType.Doctor, "Доктор" }
        };

        public static string GetString(UserType userType)
        {
            return Dictionary[userType];
        }
    }
}
