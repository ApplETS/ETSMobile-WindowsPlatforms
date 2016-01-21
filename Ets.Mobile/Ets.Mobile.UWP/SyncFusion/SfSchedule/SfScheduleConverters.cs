using Ets.Mobile.Entities.Signets;
using Localization;
using System;
using Windows.UI.Xaml.Data;

namespace Ets.Mobile.SyncFusion.SfSchedule
{
    public class AppointmentToDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var app = value as ScheduleVm;
            if(app != null)
            {
                var time = (app.EndDate - app.StartDate);
                var description = "";
                if(time.Hours > 0)
                {
                    description += $"{time.Hours} " + ((time.Hours > 1) ? StringResources.Hours : StringResources.Hour);
                }
                if(time.Hours > 0 && time.Minutes > 0)
                {
                    description += ", ";
                }
                if(time.Minutes > 0)
                {
                    description += $"{time.Minutes} " + ((time.Minutes > 1) ? StringResources.Minutes : StringResources.Minute);
                }
                if(!string.IsNullOrEmpty(description))
                {
                    description += " ";
                }
                description += $"({app.Location})";

                return description;
            }
            
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

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