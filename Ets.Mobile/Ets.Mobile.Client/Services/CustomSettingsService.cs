using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Akavache;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Entities.Signets.Interfaces;
using Themes;

namespace Ets.Mobile.Client.Services
{
    public class CustomSettingsService : ICustomSettingsService
    {
        public async Task<IList<T>> ApplyColorOnItemsForSemester<T>(IList<T> items, string semester, Func<T, object> nameSelector) where T : class, ICustomColor
        {
            var colors = AppColors.GetColors(items.Count);

            var ind = 0;
            foreach (var item in items)
            {
                await ApplyColorOnItemsForSemester(item, semester, nameSelector, colors[ind++]);
            }

            return items;
        }

        public async Task<T> ApplyColorOnItemsForSemester<T>(T item, string semester, Func<T, object> nameSelector, Color color) where T : class, ICustomColor
        {
            var courseName = nameSelector(item).ToString();
            var colorOfSchedule = await BlobCache.UserAccount.GetOrCreateObject(ClientKeys.ColorCourseForSemester(semester, courseName), () => new ColorVm(color));
            item.SetNewColor(colorOfSchedule);

            return item;
        }
    }
}
