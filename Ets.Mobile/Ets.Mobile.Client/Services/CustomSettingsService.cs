using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Akavache;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Entities.Signets.Interfaces;
using StoreFramework.Themes;

namespace Ets.Mobile.Client.Services
{
    public class CustomSettingsService : ICustomSettingsService
    {
        public async Task<IList<T>> ApplyColorOnCoursesForSemester<T>(IList<T> schedules, string semester, Func<T, object> courseNameSelector) where T : class, ICustomColor
        {
            var colors = AppColors.GetColors(schedules.Count);

            var ind = 0;
            foreach (var schedule in schedules)
            {
                var courseName = courseNameSelector(schedule).ToString();
                var colorOfSchedule = await BlobCache.UserAccount.GetOrCreateObject(ClientKeys.ColorCourseForSemester(semester, courseName), () => new ColorVm(colors[ind++]));
                schedule.SetNewColor(colorOfSchedule);
            }

            return schedules;
        }
    }
}
