using System;
using Windows.UI.Xaml.Data;

namespace Xaml.Converters
{
    public sealed class StringToLowerConverter : IValueConverter
    {
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
