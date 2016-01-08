using System.Linq;
using Akavache;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.Entities.Signets.Interfaces;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Themes;
using Windows.UI;
using Ets.Mobile.Entities.Moodle;

namespace Ets.Mobile.Client.Services
{
    public class CustomSettingsService : ICustomSettingsService
    {
        public async Task ApplyColorOnItemsForCoursesInitial(CourseVm[] items)
        {
            var colors = AppColors.GetColors(items.Length);

            var ind = 0;
            foreach (var item in items)
            {
                await ApplyColorOnCoursesWithinSemester(item, item.Semester, $"{item.Acronym}-{item.Group}", colors[ind++]);
            }
        }

        public async Task ApplyColorOnItemsForSchedule(ScheduleVm[] items)
        {
            foreach (var item in items)
            {
                await InnerApplyColorOnCourses(item, item.Semester, item.CourseAndGroup);
            }
        }

        public async Task ApplyColorOnItemsForMoodleCourses(MoodleCourseVm[] items)
        {
            var keys = await BlobCache.UserAccount.GetAllKeys().Select(k => k.Where(key => key.StartsWith("colorsFor_")).ToArray()).ToTask();
            
            if (!keys.Any())
            {
                return;
            }

            foreach (var item in items)
            {
                var key = keys.FirstOrDefault(k => k.Replace("colorsFor_", string.Empty).StartsWith($"{item.Semester}_{item.CourseName}-"));
                if (key != null)
                {
                    await InnerApplyColorOnCourses(item, key);
                }
                else
                {
                    await InnerApplyColorOnCoursesWithoutKeys(item, $"colorsFor_{item.Semester}_{item.CourseName}", Colors.Black);
                }
            }
        }

        public async Task ApplyColorOnItemsForMoodleCourseContents(MoodleCourseContentVm[] items, MoodleCourseVm course)
        {
            var keys = await BlobCache.UserAccount.GetAllKeys().Select(k => k.Where(key => key.StartsWith("colorsFor_")).ToArray()).ToTask();

            if (!keys.Any())
            {
                return;
            }

            foreach (var item in items)
            {
                var key = keys.FirstOrDefault(k => k.Replace("colorsFor_", string.Empty).StartsWith($"{course.Semester}_{course.CourseName}-"));
                if (key != null)
                {
                    await InnerApplyColorOnCourses(item, key);
                }
                else
                {
                    await InnerApplyColorOnCoursesWithoutKeys(item, $"colorsFor_{course.Semester}_{course.CourseName}", Colors.Black);
                }
            }
        }

        public async Task ApplyColorOnItemsForEvaluations(EvaluationsVm item, string semester, string courseAndGroup)
        {
            await InnerApplyColorOnCourses(item, semester, courseAndGroup);
        }

        private async Task InnerApplyColorOnCourses<T>(T customColorObj, string semester, string courseWithGroup) where T : ICustomColor 
        {
            var colorOfSchedule = await BlobCache.UserAccount.GetObject<ColorVm>(ClientKeys.ColorCourseForSemester(semester, courseWithGroup));
            customColorObj.SetNewColor(colorOfSchedule);
        }

        private async Task InnerApplyColorOnCourses<T>(T customColorObj, string key) where T : ICustomColor
        {
            var colorOfSchedule = await BlobCache.UserAccount.GetObject<ColorVm>(key);
            customColorObj.SetNewColor(colorOfSchedule);
        }

        private async Task ApplyColorOnCoursesWithinSemester(ICustomColor customColorObj, string semester, string courseWithGroup, Color color)
        {
            var colorVm = await BlobCache.UserAccount.GetOrCreateObject(ClientKeys.ColorCourseForSemester(semester, courseWithGroup), () => new ColorVm(color)).ToTask();
            customColorObj.SetNewColor(colorVm);
        }

        private async Task InnerApplyColorOnCoursesWithoutKeys<T>(T customColorObj, string key, Color color) where T : ICustomColor
        {
            var colorOfSchedule = await BlobCache.UserAccount.GetOrCreateObject(key, () => new ColorVm(color));
            customColorObj.SetNewColor(colorOfSchedule);
        }
    }
}