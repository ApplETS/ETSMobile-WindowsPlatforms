using System;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;
using Splat;

namespace StoreFramework.Converters
{
    public class AbridgedSemesterToFullNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (string.IsNullOrEmpty(value?.ToString()))
            {
                return value;
            }

            var str = value.ToString();

            if (string.IsNullOrEmpty(str))
            {
                return value;
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
