using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NeuroMedicine.Converters
{
    class DateTimeToTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime)value).TimeOfDay;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
