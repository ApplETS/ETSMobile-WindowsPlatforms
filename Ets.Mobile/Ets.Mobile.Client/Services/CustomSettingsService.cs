using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Entities.Signets.Interfaces;
using StoreFramework.Themes;

namespace Ets.Mobile.Client.Services
{
    public class CustomSettingsService : ICustomSettingsService
    {
        public async Task<T[]> ApplyColorOnCoursesForSemester<T>(T[] schedules, string semester, Func<T, object> courseNameSelector) where T : class, ICustomColor
        {
            var colors = AppColors.GetColors(schedules.Length);

            var ind = 0;
            foreach (var schedule in schedules)
            {
                var colorOfSchedule = await BlobCache.UserAccount.GetOrCreateObject(ClientKeys.ColorCourseForSemester(semester, courseNameSelector(schedule).ToString()), () => colors[ind++]);
                schedule.Color = colorOfSchedule;
            }

            return schedules;
        }
    }
}
