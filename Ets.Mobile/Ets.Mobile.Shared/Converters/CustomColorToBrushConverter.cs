using Ets.Mobile.Entities.Signets.Interfaces;
using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Ets.Mobile.Converters
{
    public class CustomColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var schedule = value as ICustomColor;
            if (schedule != null)
            {
                return new SolidColorBrush(new Color
                {
                    A = 0,
                    R = 0,
                    G = 0,
                    B = 0
                });
            }
            return new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}