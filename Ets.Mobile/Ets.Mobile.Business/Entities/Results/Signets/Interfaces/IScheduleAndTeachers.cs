using Ets.Mobile.Business.Entities.Signets;
using System.Collections.Generic;

namespace Ets.Mobile.Business.Entities.Results.Signets.Interfaces
{
    public interface IScheduleAndTeachers
    {
        List<Activity> Activities { get; set; }
        List<Teacher> Teachers { get; set; }
        string ErrorMessage { get; set; }
    }
}