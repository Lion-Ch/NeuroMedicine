using DataLayer.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.SqlServer.Tables
{
    public class RefPatient
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime DateBirth { get; set; }
        /// <summary>
        /// Пол
        /// </summary>
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
        /// Кем выдан
        /// </summary>
        public string PassportInfo { get; set; }
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
