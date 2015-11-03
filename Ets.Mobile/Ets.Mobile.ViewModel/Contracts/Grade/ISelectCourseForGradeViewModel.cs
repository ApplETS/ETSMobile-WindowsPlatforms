using Ets.Mobile.ViewModel.Content.Main;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ets.Mobile.ViewModel.Contracts.Grade
{
    public interface ISelectCourseForGradeViewModel
    {
        ReactiveList<GradeSummaryViewModelGroup> GradesItems { get; }
        ReactiveCommand<List<GradeSummaryViewModelGroup>> LoadGrades { get; }
    }
}
