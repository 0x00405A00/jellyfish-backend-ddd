using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Converter
{
    public class EntryControlLayoutByBindingTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Microsoft.Maui.Keyboard keyboard = Keyboard.Default;
            if(value == typeof(int) || value == typeof(double) || value == typeof(float) )
            {
                keyboard = Keyboard.Numeric;
            }
            return keyboard;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
