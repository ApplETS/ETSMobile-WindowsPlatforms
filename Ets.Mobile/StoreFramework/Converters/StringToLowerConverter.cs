using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Windows.UI.Xaml.Data;

namespace StoreFramework.Converters
{
    public class StringToLowerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = value as string;
            if (s != null)
            {
                return s.ToLower();
            }

            return value;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var s = value as string;
            if (s != null)
            {
                return s.ToLower();
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
