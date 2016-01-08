using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Entities.Moodle;
using Ets.Mobile.Entities.Signets;
using System.Linq;
using System.Threading.Tasks;

namespace Ets.Mobile.Client.Mixins
{
    public static class CustomSettingsMixins
    {
        public static async Task<CourseVm[]> ApplyCustomColors(this Task<CourseVm[]> courses, ICustomSettingsService service)
        {
            var c = await courses;
            var grouped = c.GroupBy(x => x.Semester).Select(x => x.OrderBy(y => y.Acronym)).ToArray();
            foreach (var group in grouped)
            {
                await service.ApplyColorOnItemsForCoursesInitial(group.ToArray());
            }
            return c;
        }

        public static async Task<MoodleCourseVm[]> ApplyCustomColors(this Task<MoodleCourseVm[]> courses, ICustomSettingsService service)
        {
            var c = await courses;
            await service.ApplyColorOnItemsForMoodleCourses(c);
            return c;
        }

        public static async Task<MoodleCourseContentVm[]> ApplyCustomColors(this Task<MoodleCourseContentVm[]> courses, MoodleCourseVm course, ICustomSettingsService service)
        {
            var c = await courses;
            await service.ApplyColorOnItemsForMoodleCourseContents(c, course);
            return c;
        }

        public static async Task<ScheduleVm[]> ApplyCustomColors(this Task<ScheduleVm[]> courses, ICustomSettingsService service)
        {
            var c = await courses;
            await service.ApplyColorOnItemsForSchedule(c);
            return c;
        }

        public static async Task<EvaluationsVm> ApplyCustomColors(this Task<EvaluationsVm> evaluation, ICustomSettingsService service, CourseVm belongsToCourse)
        {
            var eval = await evaluation;
            await service.ApplyColorOnItemsForEvaluations(eval, belongsToCourse.Semester, $"{belongsToCourse.Acronym}-{belongsToCourse.Group}");
            return eval;
        }
    }
}