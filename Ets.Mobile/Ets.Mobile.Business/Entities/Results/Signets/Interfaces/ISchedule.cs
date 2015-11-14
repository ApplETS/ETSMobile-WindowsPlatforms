using System.Collections.Generic;
using Ets.Mobile.Business.Entities.Signets;

namespace Ets.Mobile.Business.Entities.Results.Signets.Interfaces
{
    public interface ISchedule
    {
        List<Schedule> Schedules { get; set; }
        string ErrorMessage { get; set; }
    }
}