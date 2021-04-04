using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace NeuroMedicine.Converters
{
    public class DateBirthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dateBirth = (DateTime)value; 
            var age = DateTime.Now.Year - dateBirth.Year;
            if (DateTime.Now.DayOfYear < dateBirth.DayOfYear) 
                age++;
            return age.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
