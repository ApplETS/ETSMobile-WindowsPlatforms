using System;
using Windows.UI.Xaml.Data;

namespace Xaml.Converters
{
    public class DebugProxyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
#if DEBUG
            return value;
#endif
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
#if DEBUG
            return value;
#endif
        }
    }
}
