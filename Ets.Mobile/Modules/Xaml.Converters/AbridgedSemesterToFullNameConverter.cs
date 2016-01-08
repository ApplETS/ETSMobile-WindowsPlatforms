using Splat;
using System;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;

namespace Xaml.Converters
{
    public sealed class AbridgedSemesterToFullNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var str = value?.ToString();

            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            if (str == "N/A")
            {
                return str;
            }

            var semesterSeason = string.Empty;
            var resources = Locator.Current.GetService<ResourceLoader>();
            switch (str[0])
            {
                case 'A':
                    semesterSeason = resources.GetString("Autumn");
                    break;
                case 'H':
                    semesterSeason = resources.GetString("Winter");
                    break;
                case 'É':
                case 'E':
                    semesterSeason = resources.GetString("Summer");
                    break;
            }

            str = $"{semesterSeason} {str.Remove(0, 1)}";

            return str;
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}