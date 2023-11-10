using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Converter
{
    public class CollectionSelectionModeWhenBindIsTrueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SelectionMode selectionMode = SelectionMode.Single;
            bool val = (bool)value;
            if(val)
            {
                selectionMode = SelectionMode.Multiple;
            }
            return selectionMode;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null) return null;
            SelectionMode selectionMode = (SelectionMode)value;
            if(selectionMode == SelectionMode.Single)
            {
                return false;
            }
            else if(selectionMode == SelectionMode.Multiple)
            {
                return true;
            }
            return false;
        }
    }
}
