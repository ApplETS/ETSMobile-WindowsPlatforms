using System;
using Windows.UI.Xaml.Data;

namespace Ets.Mobile.Converters.SfSchedule
{
    public class StartDateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var appointment = value as DateTime?;
            if (appointment != null)
            {
                return appointment.Value.ToString("h:mm tt");
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}