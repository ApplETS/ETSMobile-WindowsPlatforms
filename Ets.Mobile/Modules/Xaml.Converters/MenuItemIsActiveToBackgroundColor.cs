using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Xaml.Converters
{
    public sealed class MenuItemIsActiveToBackgroundColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value.Equals(parameter))
            {
                return new SolidColorBrush(Color.FromArgb(255, 126, 45, 49));
            }

            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
