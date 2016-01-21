using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Content.Main;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;
using System.Collections.Generic;

namespace Ets.Mobile.ViewModel.Contracts.Main
{
    public interface IMainPageViewModel
    {
        ReactivePresenterCommand<ScheduleVm[]> LoadCoursesForToday { get; }
        IReactiveDerivedList<ScheduleVm> Today { get; }
        ReactiveList<ScheduleVm> TodayItems { get; }
        ReactivePresenterCommand<List<GradeSummaryViewModelGroup>> LoadCoursesSummaries { get; }
        IReactiveDerivedList<GradeSummaryViewModelGroup> Grades { get; }
        ReactiveList<GradeSummaryViewModelGroup> GradesItems { get; }

    }
}