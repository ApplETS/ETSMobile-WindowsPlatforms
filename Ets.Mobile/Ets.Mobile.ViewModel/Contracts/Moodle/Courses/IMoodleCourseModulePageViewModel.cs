using Ets.Mobile.Entities.Moodle;
using Ets.Mobile.ViewModel.Content.Moodle.Courses.Content;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;
using System.Collections.Generic;

namespace Ets.Mobile.ViewModel.Contracts.Moodle.Courses
{
    public interface IMoodleCourseModulePageViewModel
    {
        MoodleCourseVm Course { get; set; }
        MoodleCourseContentVm CourseContent { get; set; }
        IReactiveDerivedList<MoodleCourseModuleSummaryViewModel> CoursesModules { get; }
        ReactivePresenterCommand<List<MoodleCourseModuleSummaryViewModel>> LoadCoursesModule { get; set; }
        ReactiveList<MoodleCourseModuleSummaryViewModel> CoursesModulesItems { get; set; }
    }
}