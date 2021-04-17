using DataLayer.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.SqlServer.Tables
{
    /// <summary>
    /// Запись пациента на прием к врачу
    /// </summary>
    public class RefReception
    {
        public int Id { get; set; }
        public int RefPatientId { get; set; }
        public RefPatient RefPatient { get; set; }
        public bool IsActive { get; set; }
        public int RefServiceId { get; set; }
        public RefService RefService { get; set; }
        public int RefUserId { get; set; }
        public RefUser RefUser { get; set; }
        public DateTime DateRecording { get; set; }
    }
}
