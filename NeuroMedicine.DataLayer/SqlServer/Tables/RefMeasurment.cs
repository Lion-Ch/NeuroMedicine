using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.SqlServer.Tables
{
    /// <summary>
    /// Измерение основных показателей человека
    /// </summary>
    public class RefMeasurment
    {
        public int Id { get; set; }
        public int RefPatientId { get; set; }
        /// <summary>
        /// Пациент
        /// </summary>
        public RefPatient RefPatient { get; set; }

        public int RefUserId { get; set; }
        /// <summary>
        /// Врач, поставивший диагноз
        /// </summary>
        public RefUser RefUser { get; set; }

        public DateTime Date { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public int? Pulse { get; set; }
        public int? PressureSystolic { get; set; }
        public int? PressureDiastolic { get; set; }
    }
}
