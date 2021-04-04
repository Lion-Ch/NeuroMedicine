using DataLayer.Models.Enums;
using Neuromedicine.Core.NotifyPropertyChanged;
using System;

namespace DataLayer.Models.Classes
{
    public class Patient
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateBirth { get; set; }
        public GenderType Gender { get; set; }
        /// <summary>
        /// Мобильный телефон
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// Адрес почты
        /// </summary>
        public string Mail { get; set; }
        /// <summary>
        /// Адрес проживания
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Паспорт
        /// </summary>
        public string Passport { get; set; }
        /// <summary>
        /// СНИЛС
        /// </summary>
        public string Snills { get; set; }
        /// <summary>
        /// Полис
        /// </summary>
        public string Policy { get; set; }
    }
}
