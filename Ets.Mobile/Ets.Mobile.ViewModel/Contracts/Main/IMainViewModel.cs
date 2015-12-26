using System.Collections.Generic;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Content.Main;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;
using ReactiveUI.Xaml.Controls.Handlers;

namespace Ets.Mobile.ViewModel.Contracts.Main
{
    public interface IMainViewModel
    {
        ReactivePresenterCommand<ScheduleVm[]> LoadCoursesForToday { get; }
        ReactivePresenterCommand<List<GradeSummaryViewModelGroup>> LoadGrades { get; }
        IReactivePresenterHandler<IReactiveDerivedList<GradeSummaryViewModelGroup>> GradesPresenter { get; }
        IReactivePresenterHandler<IReactiveDerivedList<ScheduleVm>> TodayPresenter { get; }
    }
}