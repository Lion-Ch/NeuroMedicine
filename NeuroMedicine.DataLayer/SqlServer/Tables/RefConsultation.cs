using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.SqlServer.Tables
{
    public class RefConsultation
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

        public int? RefReceptionId { get; set; }
        public RefReception RefReception { get; set; }
        /// <summary>
        /// Дата обследования
        /// </summary>
        public DateTime Date { get; set; }

        public int RefServiceId { get; set; }
        public RefService RefService { get; set; }

        /// <summary>
        /// Диагноз врача
        /// </summary>
        public int RefDiagnosisId { get; set; }
        public RefDiagnosis RefDiagnosis { get; set; }

        /// <summary>
        /// Заключение врача
        /// </summary>
        public string Conclusion { get; set; }
    }
}
