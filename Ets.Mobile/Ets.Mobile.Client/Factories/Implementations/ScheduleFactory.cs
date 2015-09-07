using System.Collections.Generic;
using System.Linq;
using Ets.Mobile.Business.Entities.Results.Signets;
using Ets.Mobile.Client.Factories.Interfaces;
using Ets.Mobile.Entities.Signets;

namespace Ets.Mobile.Client.Factories.Implementations
{
    public class ScheduleFactory : IScheduleFactory
    {
        public List<ScheduleVm> Create(ScheduleResult result)
        {
            return new List<ScheduleVm>(result.Schedules.Select(schedule => new ScheduleVm()
            {
                CourseAndGroup = schedule.CourseAndGroup,
                Description = schedule.Description,
                EndDate = schedule.EndDate,
                Location = schedule.Location,
                Name = schedule.Name,
                StartDate = schedule.StartDate,
                Title = schedule.Title
            }));
        }
    }
}
