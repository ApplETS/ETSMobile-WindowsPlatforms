using Ets.Mobile.ViewModel.Content.Moodle.Courses;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;
using System.Collections.Generic;

namespace Ets.Mobile.ViewModel.Contracts.Moodle
{
    public interface IMoodleMainPageViewModel
    {
        IReactiveDerivedList<MoodleCourseSummaryViewModelGroup> Courses { get; }
        ReactivePresenterCommand<List<MoodleCourseSummaryViewModelGroup>> LoadCourses { get; set; }
        ReactiveList<MoodleCourseSummaryViewModelGroup> CoursesItems { get; set; }
    }
}