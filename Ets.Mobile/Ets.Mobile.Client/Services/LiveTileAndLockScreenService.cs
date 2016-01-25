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
        public static async Task<ScheduleForLiveTile[]> GetCurrentOrIncomingCourses()
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
                    .Select(courses => courses.Where(c => c.StartDate.Date == now.Date && c.EndDate > now.Date).OrderBy(x => x.StartDate).ThenBy(x => x.Location))
                    // Convert to simple object
                    .Select(scheduleItems => scheduleItems != null ? scheduleItems.Select(scheduleItem => new ScheduleForLiveTile(scheduleItem.ActivityName, scheduleItem.CourseAndGroup, scheduleItem.Location, scheduleItem.Name, scheduleItem.Group, scheduleItem.StartDate, scheduleItem.EndDate)).ToArray() : new ScheduleForLiveTile[] {})
                    .ToTask();
        }

        public static async Task<ScheduleForLiveTile[]> GetFollowingDayCourses()
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
                    .Select(courses => courses.Where(c => c.StartDate.Date == now.Date.AddDays(1)).OrderBy(x => x.StartDate).ThenBy(x => x.Location))
                    // Convert to simple object
                    .Select(scheduleItems => scheduleItems != null ? scheduleItems.Select(scheduleItem => new ScheduleForLiveTile(scheduleItem.ActivityName, scheduleItem.CourseAndGroup, scheduleItem.Location, scheduleItem.Name, scheduleItem.Group, scheduleItem.StartDate, scheduleItem.EndDate)).ToArray() : new ScheduleForLiveTile[] { })
                    .ToTask();
        }
    }
}