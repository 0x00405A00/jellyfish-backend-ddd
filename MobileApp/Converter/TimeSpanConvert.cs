using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Converter
{
    public class TimeSpanConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null && value is TimeSpan)
            {
                TimeSpan workValue = (TimeSpan)value;
                var totalSec = workValue.TotalSeconds;
                var calcMin = Math.Floor(totalSec / 60.0);
                string response = ((int)calcMin).ToString("000") +":"+workValue.Seconds.ToString("00") + " min.";
                return response;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
