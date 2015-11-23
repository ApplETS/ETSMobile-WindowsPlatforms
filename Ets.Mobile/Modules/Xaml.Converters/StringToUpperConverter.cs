using System;
using Windows.UI.Xaml.Data;

namespace Xaml.Converters
{
    public sealed class StringToUpperConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var s = value as string;
            return s != null ? s.ToUpper() : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}