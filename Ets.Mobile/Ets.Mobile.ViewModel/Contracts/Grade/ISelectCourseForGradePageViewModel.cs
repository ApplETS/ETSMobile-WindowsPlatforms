using Ets.Mobile.ViewModel.Content.Main;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;
using System.Collections.Generic;

namespace Ets.Mobile.ViewModel.Contracts.Grade
{
    public interface ISelectCourseForGradeViewModel
    {
        ReactiveList<GradeSummaryViewModelGroup> GradesItems { get; }
        ReactivePresenterCommand<List<GradeSummaryViewModelGroup>> LoadCoursesSummaries { get; }
    }
}