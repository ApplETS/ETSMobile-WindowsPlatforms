using System;
using System.Text.RegularExpressions;
using Windows.UI.Xaml.Data;

namespace Xaml.Converters
{
    public class HtmlStringToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var str = value?.ToString();
            if (value == null || string.IsNullOrEmpty(str))
            {
                return value;
            }
            return Regex.Replace(str, @"<[^>]+>|&nbsp;", "").Trim();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class HtmlStringToStringExcludingParameterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var str = value?.ToString();
            if (value == null || string.IsNullOrEmpty(str))
            {
                return value;
            }
            if (parameter != null && str == parameter.ToString())
            {
                return string.Empty;
            }
            return Regex.Replace(str, @"<[^>]+>|&nbsp;", "").Trim();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}