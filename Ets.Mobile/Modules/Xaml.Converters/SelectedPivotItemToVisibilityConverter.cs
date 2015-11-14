using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Xaml.Converters
{
    public sealed class SelectedPivotItemToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return null;
            }

            bool val;

            if (bool.TryParse(value.ToString(), out val))
            {
                return !val ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
