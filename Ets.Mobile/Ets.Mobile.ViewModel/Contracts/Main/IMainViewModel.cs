using System.Collections.Generic;
using System.Reactive;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Content.Main;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.ViewModel;

namespace Ets.Mobile.ViewModel.Contracts.Main
{
    public interface IMainViewModel
    {
        ReactiveCommand<ScheduleVm[]> LoadCoursesForToday { get; }
        ReactiveCommand<List<GradeSummaryViewModelGroup>> LoadGrades { get; }
        ReactiveCommand<Unit> NavigateToSchedule { get; }
        ReactiveCommand<Unit> NavigateToProgram { get; }
        IReactivePresenterViewModel<ReactiveList<GradeSummaryViewModelGroup>> GradesPresenter { get; }
        IReactivePresenterViewModel<ReactiveList<ScheduleVm>> TodayPresenter { get; }        
    }
}
