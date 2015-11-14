using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Xaml.Converters
{
    public sealed class ScreenWidthMultiplier : IValueConverter
    {
        public ScreenWidthMultiplier()
        {
            Percentage = 0.9;//default 90% of screenwidth. 
            MinWidth = 0;
        }

        public double MinWidth { get; set; }

        public double Percentage { get; set; }
        
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var width = Window.Current.CoreWindow.Bounds.Width * Percentage *
                   global::System.Convert.ToDouble(parameter);

            return width >= MinWidth ? width : MinWidth;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
