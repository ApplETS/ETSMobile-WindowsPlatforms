using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;
using Splat;

namespace StoreFramework.Converters
{
    public class StringToLocalizedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var resourceLoader = Locator.Current.GetService<ResourceLoader>();
            var val = resourceLoader.GetString(value.ToString());

            if(string.IsNullOrEmpty(val))
            {
                throw new KeyNotFoundException($"The Key '{value}' couldn't be found in the Resources");
            }

            return val;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
