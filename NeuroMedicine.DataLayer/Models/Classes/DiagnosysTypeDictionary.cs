using DataLayer.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Classes
{
    internal class DiagnosysTypeDictionary
    {
        public Dictionary<DiagnosysType, string> Dictionary;

        public DiagnosysTypeDictionary()
        {
            Dictionary = new Dictionary<DiagnosysType, string>()
            {
                {DiagnosysType.NoPathology, "Без патологий" },
                {DiagnosysType.Pneumonia, "Пневмония" },
                {DiagnosysType.Tuberculosis, "Туберкулез" },
                {DiagnosysType.Bronchitis, "Бронхит" },
                {DiagnosysType.Cancer, "Рак" },
                {DiagnosysType.MultipleDiseases, "Несколько заболеваний" },

            };
        }

        public string GetString(DiagnosysType userType)
        {
            return Dictionary[userType];
        }
    }
}
