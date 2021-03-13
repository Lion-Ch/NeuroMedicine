using DataLayer.Models.Enums;
using System.Collections.Generic;

namespace DataLayer.Models.Classes
{
    public class DiagnosticTypeDictionary
    {
        public Dictionary<DiagnosticType, string> Dictionary { get; }

        public DiagnosticTypeDictionary()
        {
            Dictionary = new Dictionary<DiagnosticType, string>()
            {
                {DiagnosticType.Lung, "Диагностика легких" },
                {DiagnosticType.Bon, "Диагностика переломов" }
            };
        }
    }
}
