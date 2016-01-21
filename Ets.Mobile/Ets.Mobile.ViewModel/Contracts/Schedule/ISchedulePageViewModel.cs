using Ets.Mobile.Entities.Signets;
using ReactiveUI;
using System.Collections.Generic;

namespace Ets.Mobile.ViewModel.Contracts.Schedule
{
    public interface ISchedulePageViewModel
    {
        ReactiveList<ScheduleVm> ScheduleItems { get; }
        ReactiveCommand<IEnumerable<ScheduleVm>> FetchSchedule { get; }
    }
}