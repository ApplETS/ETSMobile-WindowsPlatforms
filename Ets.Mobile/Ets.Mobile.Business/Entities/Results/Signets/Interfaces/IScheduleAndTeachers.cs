using System.Collections.Generic;
using Ets.Mobile.Business.Entities.Signets;

namespace Ets.Mobile.Business.Entities.Results.Signets.Interfaces
{
    public interface IScheduleAndTeachers
    {
        List<Activity> Activities { get; set; }
        List<Teacher> Teachers { get; set; }
        string ErrorMessage { get; set; }
    }
}