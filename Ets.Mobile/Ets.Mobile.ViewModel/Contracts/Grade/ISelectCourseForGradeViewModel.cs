using Ets.Mobile.ViewModel.Content.Main;
using ReactiveUI;
using System.Collections.Generic;
using ReactiveUI.Xaml.Controls.Core;

namespace Ets.Mobile.ViewModel.Contracts.Grade
{
    public interface ISelectCourseForGradeViewModel
    {
        ReactiveList<GradeSummaryViewModelGroup> GradesItems { get; }
        ReactivePresenterCommand<List<GradeSummaryViewModelGroup>> LoadGrades { get; }
    }
}