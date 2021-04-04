using DataLayer.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.SqlServer.Tables
{
    /// <summary>
    /// Диагнозы пациента
    /// </summary>
    public class RefPatientDiagnosis
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

        /// <summary>
        /// Дата обследования
        /// </summary>
        public DateTime DatePhoto { get; set; }

        /// <summary>
        /// Тип диагностики
        /// </summary>
        public DiagnosticType DiagnosticType { get; set; }

        /// <summary>
        /// Диагноз врача
        /// </summary>
        public DiagnosysType DiagnosysType { get; set; }

        /// <summary>
        /// Заключение врача
        /// </summary>
        public string Conclusion { get; set; }

        /// <summary>
        /// Результат нейросети
        /// </summary>
        public double ResultNeuralNetwork { get; set; }

        /// <summary>
        /// Путь к снимку
        /// </summary>
        public string PhotoUrl { get; set; }
    }
}
