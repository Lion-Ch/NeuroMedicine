using Neuromedicine.Core.NotifyPropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Settings
{
    public class Settings: BaseNotifyPropertyChanged
    {
        public int Id { get; set; }
        public string NameOrganization { get; set; } = "Медицинский центр \"Пульс\"";
        public string fioDirector { get; set; } = "Иванов Иван Иванович";
        public string cityOrgTag { get; set; } = "Новочеркасск";
        public string addressOrgTag { get; set; } = "Новочеркасск, улица Московская, д.60";
        public string indexOrgTag { get; set; } = "346234";
        public string phoneOrgTag { get; set; } = "8 800 555 35 35";
        public string innOrgTag { get; set; } = "12345678901234567890";
        public string rsOrgTag { get; set; } = "123456789012";
        public string bankOrgTag { get; set; } = "Сбербанк";
        public string ksOrgTag { get; set; } = "123456789012";
        public string bikOrgTag { get; set; } = "1234567890";
        public string PathToNeuro { get; set; } = @"C:\Users\levac\OneDrive\Рабочий стол\Учеба\Диплом\Модель";
    }
}
