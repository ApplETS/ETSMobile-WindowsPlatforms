using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace StoreFramework.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return value;
            }

            bool val;

            if (bool.TryParse(value.ToString(), out val))
            {
                return val ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Visible;
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
