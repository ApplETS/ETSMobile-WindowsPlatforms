using Ets.Mobile.Entities.Moodle;
using Ets.Mobile.ViewModel.Content.Moodle.Courses.Content;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;
using System.Collections.Generic;

namespace Ets.Mobile.ViewModel.Contracts.Moodle.Courses
{
    public interface IMoodleCourseModuleContentPageViewModel
    {
        MoodleCourseVm Course { get; set; }
        MoodleCourseContentVm CourseContent { get; set; }
        MoodleCourseModuleVm CourseModule { get; set; }
        IReactiveDerivedList<MoodleCourseModuleContentSummaryViewModel> CoursesModuleContents { get; }
        ReactivePresenterCommand<List<MoodleCourseModuleContentSummaryViewModel>> LoadCoursesModuleContent { get; set; }
        ReactiveList<MoodleCourseModuleContentSummaryViewModel> CoursesModuleContentsItems { get; set; }
    }
}