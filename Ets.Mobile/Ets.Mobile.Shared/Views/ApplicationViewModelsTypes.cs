﻿using System;
using Ets.Mobile.ViewModel.Pages.Grade;
using Ets.Mobile.ViewModel.Pages.Main;
using Ets.Mobile.ViewModel.Pages.Program;
using Ets.Mobile.ViewModel.Pages.Schedule;
using Ets.Mobile.ViewModel.Pages.Settings;

namespace Ets.Mobile.Views
{
    public class ApplicationViewModelsTypes
    {
        public Type Main { get; set; } = typeof(MainViewModel);
        public Type Schedule { get; set; } = typeof(ScheduleViewModel);
        public Type SelectCourseForGrade { get; set; } = typeof(SelectCourseForGradeViewModel);
        public Type Program { get; set; } = typeof(ProgramViewModel);
        public Type Settings { get; set; } = typeof(SettingsViewModel);
    }
}
