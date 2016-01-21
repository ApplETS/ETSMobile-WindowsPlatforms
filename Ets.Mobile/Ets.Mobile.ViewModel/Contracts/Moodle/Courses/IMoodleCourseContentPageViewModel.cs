using Ets.Mobile.Entities.Moodle;
using Ets.Mobile.ViewModel.Content.Moodle.Courses.Content;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;
using System.Collections.Generic;

namespace Ets.Mobile.ViewModel.Contracts.Moodle.Courses
{
    public interface IMoodleCourseContentPageViewModel
    {
        MoodleCourseVm Course { get; set; }
        IReactiveDerivedList<MoodleCourseContentSummaryViewModel> CoursesContent { get; }
        ReactivePresenterCommand<List<MoodleCourseContentSummaryViewModel>> LoadCoursesContent { get; set; }
        ReactiveList<MoodleCourseContentSummaryViewModel> CoursesContentItems { get; set; }
    }
}