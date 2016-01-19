using Splat;
using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace Xaml.Converters
{
    public class SplatBitmapToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var bitmap = value as IBitmap;
            if (bitmap != null)
            {
                return bitmap.ToNative();
            }
            else if(!string.IsNullOrEmpty(parameter?.ToString()))
            {
                return new BitmapImage(new Uri(parameter.ToString()));
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}