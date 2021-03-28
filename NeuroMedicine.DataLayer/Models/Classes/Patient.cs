using Neuromedicine.Core.NotifyPropertyChanged;
using System;

namespace DataLayer.Models.Classes
{
    public class Patient
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateBirth { get; set; }
    }
}
