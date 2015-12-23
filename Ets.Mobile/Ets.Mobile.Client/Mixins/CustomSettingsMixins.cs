using System;
using System.Linq;
using System.Reactive.Linq;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Entities.Signets;

namespace Ets.Mobile.Client.Mixins
{
    public static class CustomSettingsMixins
    {
        public static IObservable<CourseVm[]> ApplyCustomColors(this IObservable<CourseVm[]> courses, ICustomSettingsService service)
        {
            return courses
                .Do(async c =>
                {
                    var grouped = c.GroupBy(x => x.Semester).Select(x => x.OrderBy(y => y.Acronym)).ToArray();
                    foreach (var group in grouped)
                    {
                        await service.ApplyColorOnItemsForCoursesInitial(group.ToArray());
                    }
                });
        }

        public static IObservable<ScheduleVm[]> ApplyCustomColors(this IObservable<ScheduleVm[]> courses, ICustomSettingsService service)
        {
            return courses
                .Do(async c =>
                {
                    await service.ApplyColorOnItemsForSchedule(c);
                });
        }

        public static IObservable<EvaluationsVm> ApplyCustomColors(this IObservable<EvaluationsVm> evaluation, ICustomSettingsService service, CourseVm belongsToCourse)
        {
            return evaluation
                .Do(async eval =>
                {
                    await service.ApplyColorOnItemsForEvaluations(eval, belongsToCourse.Semester, $"{belongsToCourse.Acronym}-{belongsToCourse.Group}");
                });
        }
    }
}