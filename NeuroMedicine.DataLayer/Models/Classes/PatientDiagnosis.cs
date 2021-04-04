using DataLayer.Models.Enums;
using DataLayer.SqlServer.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Classes
{
    public class PatientDiagnosis
    {
        public int Id { get; set; }

        public int RefPatientId { get; set; }
        /// <summary>
        /// Пациент
        /// </summary>
        public Patient RefPatient { get; set; }

        public int RefUserId { get; set; }
        /// <summary>
        /// Врач, поставивший диагноз
        /// </summary>
        public User RefUser { get; set; }

        public int? RefReceptionId { get; set; }
        /// <summary>
        /// Запись на прием
        /// </summary>
        public Reception RefReception { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Дата снимка
        /// </summary>
        public DateTime DatePhoto { get; set; }

        /// <summary>
        /// Тип диагностики
        /// </summary>
        public DiagnosticType DiagnosticType { get; set; }

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
