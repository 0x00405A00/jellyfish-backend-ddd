using MobileApp.Model;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Converter
{
    public class MessageColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) 
                return null;


            var send = Microsoft.Maui.Graphics.Color.FromHex("#05A300");
            var received = Microsoft.Maui.Graphics.Color.FromHex("#7EA4FC");
            if (parameter != null && parameter.GetType().IsArray)
            {
                var array = (Array) parameter;  
                if(array.Length == 2)
                {
                    
                    send = Microsoft.Maui.Graphics.Color.FromHex(array.GetValue(0).ToString());
                    received = Microsoft.Maui.Graphics.Color.FromHex(array.GetValue(1).ToString());
                }
            }


            var item = (bool)value;

            return item?received:send;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
