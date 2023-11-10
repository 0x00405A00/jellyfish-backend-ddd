using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Converter
{
    public class DateTimeConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dateTime = (DateTime)value;
            string dateTimeStr = null;
            if (DateTime.Now.Day > dateTime.Day && DateTime.Now.Year == dateTime.Year)
            {
                dateTimeStr = dateTime.ToString("d.MMM");
            }
            else if (DateTime.Now.Day == dateTime.Day && DateTime.Now.Year == dateTime.Year)
            {
                dateTimeStr = dateTime.ToString("HH:mm");
            }
            else
            {
                dateTimeStr = dateTime.ToString("dd.MM.yyyy");
            }
            return dateTimeStr;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
