using System.Collections.Generic;
using System.Linq;
using Ets.Mobile.Business.Entities.Results.Signets;
using Ets.Mobile.Client.Factories.Interfaces;
using Ets.Mobile.Entities.Signets;

namespace Ets.Mobile.Client.Factories.Implementations
{
    public class ScheduleFinalExamsFactory : IScheduleFinalExamsFactory
    {
        public List<ScheduleFinalExamVm> Create(ScheduleFinalExamsResult result)
        {
            return new List<ScheduleFinalExamVm>(result.ScheduleFinalExams.Select(schedule => new ScheduleFinalExamVm()
            {
                Abridged = schedule.Abridged,
                Date = schedule.Date,
                EndHour = schedule.EndHour,
                Group = schedule.Group,
                Location = schedule.Location,
                StartHour = schedule.StartHour
            }));
        }
    }
}
