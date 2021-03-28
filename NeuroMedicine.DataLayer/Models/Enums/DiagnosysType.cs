using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Enums
{
    public enum DiagnosysType
    {
        /// <summary>
        /// Здоров
        /// </summary>
        NoPathology = 0,
        /// <summary>
        /// Пневмония
        /// </summary>
        Pneumonia =1,
        /// <summary>
        /// Туберкулез
        /// </summary>
        Tuberculosis = 2,
        /// <summary>
        /// Бронхит
        /// </summary>
        Bronchitis = 3,
        /// <summary>
        /// Рак
        /// </summary>
        Cancer = 4,
        /// <summary>
        /// Несколько заболеваний
        /// </summary>
        MultipleDiseases=5
    }
}
