using MobileApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Converter
{
    public class LanguageSelectPrefixStringConveter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            LanguageModel response = null;
            if (value != null && value is string && parameter != null && parameter is Picker)
            {
                string input = value.ToString();

                var obj = (Picker)parameter;
                if(obj != null && obj.ItemsSource != null)
                {
                    var collection = (IList<LanguageModel>)obj.ItemsSource;
                    response = collection.ToList().Find(x=> x.PhonePrefix == input);
                }

            }
            
            return response;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string response = null;
            LanguageModel tmp = null;
            if (value != null && value is LanguageModel)
            {
                tmp = ((LanguageModel)(value));
                response = tmp.PhonePrefix;
            }
            return response;
        }
    }
}
