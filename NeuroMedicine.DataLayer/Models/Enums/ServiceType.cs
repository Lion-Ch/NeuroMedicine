using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Enums
{
    /// <summary>
    /// Услуги оказываемые медцентром
    /// </summary>
    public enum ServiceType
    {
        /// <summary>
        /// Консультация
        /// </summary>
        Consultation = 1,
        /// <summary>
        /// Флюрография
        /// </summary>
        Fluorography = 2,
        /// <summary>
        /// Анализ крови
        /// </summary>
        BloodTest = 3,
        /// <summary>
        /// Анализ мочи
        /// </summary>
        AnalysisOfUrine = 4,
        /// <summary>
        /// ЭКГ
        /// </summary>
        ECG = 5
    }
}
