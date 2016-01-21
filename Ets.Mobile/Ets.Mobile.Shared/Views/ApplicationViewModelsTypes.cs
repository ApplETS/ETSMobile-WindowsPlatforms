using Ets.Mobile.ViewModel.Pages.Grade;
using Ets.Mobile.ViewModel.Pages.Main;
using Ets.Mobile.ViewModel.Pages.Moodle;
using Ets.Mobile.ViewModel.Pages.Program;
using Ets.Mobile.ViewModel.Pages.Schedule;
using Ets.Mobile.ViewModel.Pages.Settings;
using System;

namespace Ets.Mobile.Views
{
    public class ApplicationViewModelsTypes
    {
        public Type Main { get; set; } = typeof(MainPageViewModel);
        public Type Schedule { get; set; } = typeof(ScheduleViewModel);
        public Type SelectCourseForGrade { get; set; } = typeof(SelectCourseForGradePageViewModel);
        public Type Program { get; set; } = typeof(ProgramPageViewModel);
        public Type Moodle { get; set; } = typeof(MoodleMainPageViewModel);
        public Type Settings { get; set; } = typeof(SettingsViewModel);
    }
}