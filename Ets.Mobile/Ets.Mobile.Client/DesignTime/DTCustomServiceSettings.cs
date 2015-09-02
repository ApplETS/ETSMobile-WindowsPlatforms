using System;
using System.Threading.Tasks;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Entities.Signets.Interfaces;
using StoreFramework.Themes;

namespace Ets.Mobile.Client.DesignTime
{
    public class DtCustomServiceSettings : ICustomSettingsService
    {
        public Task<T[]> ApplyColorOnCoursesForSemester<T>(T[] schedules, string semester, Func<T, object> courseNameSelector) where T : class, ICustomColor
        {
            var colors = AppColors.GetColors(schedules.Length);

            var ind = 0;
            foreach (var schedule in schedules)
            {
                schedule.Color = colors[ind++];
            }

            return Task.FromResult(schedules);
        }
    }
}
