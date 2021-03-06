﻿using Akavache;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Entities.Moodle;
using Ets.Mobile.Entities.Shared;
using Ets.Mobile.Entities.Signets;
using Splat;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Themes.Contracts;
using Themes.Entities;

namespace Ets.Mobile.Client.Services
{
    public class CustomSettingsService : ICustomSettingsService
    {
        public async Task ApplyColorOnItemsForCoursesInitial(CourseVm[] items)
        {
            var colors = Locator.Current.GetService<IAppColors>().GetColors(items.Length);

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
            var keys = await Locator.Current.GetService<IBlobCache>().GetAllKeys().Select(k => k.Where(key => key.StartsWith("colorsFor_")).ToArray()).ToTask();
            
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
                    await InnerApplyColorOnCoursesWithoutKeys(item, $"colorsFor_{item.Semester}_{item.CourseName}", new ColorAsString("#000000"));
                }
            }
        }

        public async Task ApplyColorOnItemsForMoodleCourseContents(MoodleCourseContentVm[] items, MoodleCourseVm course)
        {
            var keys = await Locator.Current.GetService<IBlobCache>().GetAllKeys().Select(k => k.Where(key => key.StartsWith("colorsFor_")).ToArray()).ToTask();

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
                    await InnerApplyColorOnCoursesWithoutKeys(item, $"colorsFor_{course.Semester}_{course.CourseName}", new ColorAsString("#000000"));
                }
            }
        }

        public async Task ApplyColorOnItemsForEvaluations(EvaluationsVm item, string semester, string courseAndGroup)
        {
            await InnerApplyColorOnCourses(item, semester, courseAndGroup);
        }

        private async Task InnerApplyColorOnCourses<T>(T customColorObj, string semester, string courseWithGroup) where T : ICustomColor 
        {
            var colorOfSchedule = await Locator.Current.GetService<IBlobCache>().GetObject<ColorVm>(ClientKeys.ColorCourseForSemester(semester, courseWithGroup));
            customColorObj.SetNewColor(colorOfSchedule);
        }

        private async Task InnerApplyColorOnCourses<T>(T customColorObj, string key) where T : ICustomColor
        {
            var colorOfSchedule = await Locator.Current.GetService<IBlobCache>().GetObject<ColorVm>(key);
            customColorObj.SetNewColor(colorOfSchedule);
        }

        private async Task ApplyColorOnCoursesWithinSemester(ICustomColor customColorObj, string semester, string courseWithGroup, ColorAsString color)
        {
            var colorVm = await Locator.Current.GetService<IBlobCache>().GetOrCreateObject(ClientKeys.ColorCourseForSemester(semester, courseWithGroup), () => new ColorVm(color.HexColor)).ToTask();
            customColorObj.SetNewColor(colorVm);
        }

        private async Task InnerApplyColorOnCoursesWithoutKeys<T>(T customColorObj, string key, ColorAsString color) where T : ICustomColor
        {
            var colorOfSchedule = await Locator.Current.GetService<IBlobCache>().GetOrCreateObject(key, () => new ColorVm(color.HexColor));
            customColorObj.SetNewColor(colorOfSchedule);
        }
    }
}