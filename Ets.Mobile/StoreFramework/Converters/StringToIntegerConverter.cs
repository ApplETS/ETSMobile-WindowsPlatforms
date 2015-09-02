using System;
using Windows.UI.Xaml.Data;

namespace StoreFramework.Converters
{
    public class StringToIntegerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return null;
            }

            var str = value.ToString();

            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace(",", ".");

                double result;
                if (double.TryParse(str, out result))
                {
                    return (int)result;
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
