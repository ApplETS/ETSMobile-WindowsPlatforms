using System;
using System.Runtime.InteropServices;
using Windows.UI.Xaml.Data;

namespace Xaml.Converters
{
    public sealed class StringToNotApplicableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value == null || value?.ToString() == "")
            {
                return "N/A";
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
