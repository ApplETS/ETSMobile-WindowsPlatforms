using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml.Data;

namespace Xaml.Converters
{
    public sealed class BooleanToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = value as bool?;
            var convertedValue = (double)0;

            if (val != null && val.Value)
            {
                double res;
                if (parameter != null && double.TryParse(parameter.ToString(), out res))
                {
                    convertedValue = res;
                }
                else
                {
                    convertedValue = 1;
                }
            }

            return convertedValue;
        }

        [return: ReturnValueName("convertedBackValue")]
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}