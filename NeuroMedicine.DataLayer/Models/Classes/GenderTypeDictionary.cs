using DataLayer.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Classes
{
    public class GenderTypeDictionary
    {
        public Dictionary<GenderType, string> Dictionary;

        public GenderTypeDictionary()
        {
            Dictionary = new Dictionary<GenderType, string>()
            {
                {GenderType.Male, "Мужской" },
                {GenderType.Female, "Женский" },

            };
        }

        public string GetString(GenderType userType)
        {
            return Dictionary[userType];
        }
    }
}
