using Splat;
using System;
using Windows.UI.Xaml.Data;

namespace Xaml.Converters
{
    public class SplatIBitmapToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var bitmap = value as IBitmap;
            if (bitmap != null)
            {
                return bitmap.ToNative();
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}