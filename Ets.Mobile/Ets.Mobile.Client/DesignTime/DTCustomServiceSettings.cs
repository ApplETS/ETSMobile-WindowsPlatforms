using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Entities.Signets.Interfaces;
using StoreFramework.Themes;

namespace Ets.Mobile.Client.DesignTime
{
    public class DtCustomServiceSettings : ICustomSettingsService
    {
        public Task<IList<T>> ApplyColorOnCoursesForSemester<T>(IList<T> schedules, string semester, Func<T, object> courseNameSelector) where T : class, ICustomColor
        {
            var colors = AppColors.GetColors(schedules.Count);

            var ind = 0;
            foreach (var schedule in schedules)
            {
                schedule.SetNewColor(new ColorVm(colors[ind++]));
            }

            return Task.FromResult(schedules);
        }
    }
}
