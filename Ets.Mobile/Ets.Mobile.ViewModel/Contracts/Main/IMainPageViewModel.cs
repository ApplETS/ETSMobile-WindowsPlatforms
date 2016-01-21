using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Content.Main;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;
using ReactiveUI.Xaml.Controls.Handlers;
using System.Collections.Generic;

namespace Ets.Mobile.ViewModel.Contracts.Main
{
    public interface IMainPageViewModel
    {
        ReactivePresenterCommand<ScheduleVm[]> LoadCoursesForToday { get; }
        ReactivePresenterCommand<List<GradeSummaryViewModelGroup>> LoadCoursesSummaries { get; }
        IReactivePresenterHandler<IReactiveDerivedList<GradeSummaryViewModelGroup>> GradesPresenter { get; }
        IReactivePresenterHandler<IReactiveDerivedList<ScheduleVm>> TodayPresenter { get; }
    }
}