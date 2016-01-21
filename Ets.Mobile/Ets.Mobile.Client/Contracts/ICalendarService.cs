using Ets.Mobile.Entities.Signets;
using System;
using System.Threading.Tasks;

namespace Ets.Mobile.Client.Contracts
{
    public interface ICalendarService
    {
        Task IntegrateScheduleToCalendar(ScheduleVm[] schedule);
        Task<Tuple<bool, string>> RemoveScheduleFromCalendar();
    }
}