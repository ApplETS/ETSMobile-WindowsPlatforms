using System;
using Windows.UI.Xaml.Data;

namespace Xaml.Converters
{
    public sealed class NullOrNotToFontSizeSelectorConverter : IValueConverter
    {
        /// <summary>
        /// This is a special converter to allow the use of different fontsizes
        /// i.e.: you want different FontSize when the value is not or not.
        /// </summary>
        /// <param name="value">Value that can be null or not</param>
        /// <param name="targetType"></param>
        /// <param name="parameter">[FontWhenNotNull,FontWhenNull]</param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var parameterAsString = parameter as string;
            if (string.IsNullOrEmpty(parameterAsString))
            {
                // You need to specify the parameter
                // Parameter is: "X,Y"
                // X is the FontSize when value isn't null
                // Y is the FontSize when value is null
                throw new Exception("You forgot the parameter");
            }

            var stringNotNullOrNull = parameterAsString.Split(',');
            if (stringNotNullOrNull.Length != 2)
            {
                // Parameter should have 2 FontSizes: "X,Y"
                // X is the FontSize when value isn't null
                // Y is the FontSize when value is null
                throw new Exception("The parameter should be 'X,Y'");
            }

            var valueAsString = value as string;
            return string.IsNullOrEmpty(valueAsString) ? stringNotNullOrNull[1] : stringNotNullOrNull[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}