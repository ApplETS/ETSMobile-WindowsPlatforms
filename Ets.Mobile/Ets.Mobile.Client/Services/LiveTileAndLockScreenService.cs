using Akavache;
using Ets.Mobile.Client.Entities.Schedule;
using Ets.Mobile.Entities.Signets;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace Ets.Mobile.Client.Services
{
    public class LiveTileAndLockScreenService
    {
        public static async Task<ScheduleForLiveTile> GetCurrentOrIncomingCourse()
        {
            BlobCache.ApplicationName = "EtsMobile";
            var cache = BlobCache.UserAccount;
            var now = DateTime.Now;
            return
                await cache.GetObject<SemesterVm[]>("semesters")
                    .Where(x => x.FirstOrDefault(y => y.StartDate <= now && y.EndDate > now) != null)
                    .Select(semesters => semesters.FirstOrDefault(y => y.StartDate <= now && y.EndDate > now))
                    .SelectMany(currentSemester => cache.GetObject<ScheduleVm[]>("schedule_" + currentSemester.AbridgedName))
                    // Get Today's Courses
                    .Select(courses => courses.Where(c => c.StartDate.Date == now.Date).OrderBy(c => c.StartDate))
                    // Get Current Course or Following Course
                    .FirstOrDefaultAsync(courses => courses.Any(c => (c.StartDate <= now && c.EndDate > now) || c.StartDate > now))
                    .SelectMany(courses =>
                    {
                        var currentOrIncomingCourse =
                            courses?.FirstOrDefault(c => (c.StartDate <= now && c.EndDate > now) || c.StartDate > now);
                        return currentOrIncomingCourse != null ? Observable.Return(currentOrIncomingCourse) : Observable.Return(default(ScheduleVm));
                    })
                    // Convert to simple object
                    .Select(scheduleItem => scheduleItem != null ? new ScheduleForLiveTile(scheduleItem.ActivityName, scheduleItem.Location, scheduleItem.Name, scheduleItem.Group, scheduleItem.StartDate, scheduleItem.EndDate) : default(ScheduleForLiveTile))
                    .ToTask();
        }
    }
}