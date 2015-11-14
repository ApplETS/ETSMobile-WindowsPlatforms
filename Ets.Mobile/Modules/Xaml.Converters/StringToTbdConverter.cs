using System;
using System.Runtime.InteropServices;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;
using Splat;

namespace Xaml.Converters
{
    public sealed class StringToTbdConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value == null || value?.ToString() == "")
            {
                return Locator.Current.GetService<ResourceLoader>().GetString("ToBeDetermined");
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
