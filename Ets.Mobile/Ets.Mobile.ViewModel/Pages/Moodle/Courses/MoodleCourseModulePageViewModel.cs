using Akavache;
using Ets.Mobile.Client.Mixins;
using Ets.Mobile.Entities.Moodle;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Content.Moodle.Courses.Content;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;
using ReactiveUI.Xaml.Controls.Extensions;
using ReactiveUI.Xaml.Controls.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Ets.Mobile.ViewModel.Pages.Moodle.Courses
{
    [DataContract]
    public class MoodleCourseModulePageViewModel : ViewModelBase
    {
        public MoodleCourseModulePageViewModel(IScreen screen, MoodleCourseVm course, MoodleCourseContentVm content) : base(screen, "MoodleCourseModule")
        {
            Course = course;
            CourseContent = content;
            OnViewModelCreation();
        }

        protected sealed override void OnViewModelCreation()
        {
            _navigateToModuleItem = ReactiveCommand.CreateAsyncTask(NavigateToModuleItemImpl);

            CoursesModulesItems = new ReactiveList<MoodleCourseModuleSummaryViewModel>(
                CourseContent.Modules.Any() ? CourseContent.Modules.Select(m => new MoodleCourseModuleSummaryViewModel(Course, CourseContent, m, _navigateToModuleItem)) : new List<MoodleCourseModuleSummaryViewModel>()
            );

            CoursesModules = CoursesModulesItems.CreateDerivedCollection(x => x, x => x.Dispose());

            LoadCoursesModule = ReactivePresenterCommand.CreateAsyncObservable(_ => FetchCoursesModulesImpl());

            LoadCoursesModule.ThrownExceptions
                .Subscribe(x =>
                {
                    UserError.Throw(x.Message, x);
                });

            CoursesModulePresenter = LoadCoursesModule.CreateReactivePresenter(CoursesModulesItems, CoursesModules, true);

            if (!CoursesModulesItems.Any())
            {
                CoursesModulePresenter.OnNextEmptyMessage();
            }
        }

        private Task<Unit> NavigateToModuleItemImpl(object param)
        {
            var selectedItem = param as MoodleCourseModuleSummaryViewModel;
            if (selectedItem != null)
            {
                if (!string.IsNullOrEmpty(selectedItem.CourseModule.Url))
                {
                    RxApp.MainThreadScheduler.Schedule(() =>
                    {
                        HostScreen.Router.Navigate.Execute(new MoodleCourseModuleContentPageViewModel(HostScreen, selectedItem.Course, selectedItem.CourseContent, selectedItem.CourseModule));
                    });
                }
            }
            return Task.FromResult(Unit.Default);
        }

        private IObservable<List<MoodleCourseModuleSummaryViewModel>> FetchCoursesModulesImpl()
        {
            var fetchCoursesContent = Cache.GetAndFetchLatest(ViewModelKeys.MoodleCoursesContentForCourse(Course.Id), FetchMoodleCoursesContents)
                .FirstAsync(s => s.Any(sc => sc.Id == CourseContent.Id && sc.Modules.Length > 0));
                
            var createSummaries = 
                fetchCoursesContent
                    .Select(sc => sc.First(s => s.Id == CourseContent.Id).Modules.Select(m => new MoodleCourseModuleSummaryViewModel(Course, CourseContent, m, _navigateToModuleItem)).ToList());

            return createSummaries.ThrowIfEmpty();
        }

        private Task<MoodleCourseContentVm[]> FetchMoodleCoursesContents()
        {
            return ClientServices().MoodleService.CoursesContents(Course.Id).ApplyCustomColors(Course, SettingsService());
        }

        #region Properties

        private MoodleCourseVm _course;

        [DataMember]
        public MoodleCourseVm Course
        {
            get { return _course; }
            set { this.RaiseAndSetIfChanged(ref _course, value); }
        }

        private MoodleCourseContentVm _courseContent;

        [DataMember]
        public MoodleCourseContentVm CourseContent
        {
            get { return _courseContent; }
            set { this.RaiseAndSetIfChanged(ref _courseContent, value); }
        }

        private ReactiveCommand<Unit> _navigateToModuleItem;
        public IReactiveDerivedList<MoodleCourseModuleSummaryViewModel> CoursesModules { get; protected set; }
        public ReactivePresenterCommand<List<MoodleCourseModuleSummaryViewModel>> LoadCoursesModule { get; set; }
        public IReactivePresenterHandler<IReactiveDerivedList<MoodleCourseModuleSummaryViewModel>> CoursesModulePresenter { get; set; }
        public ReactiveList<MoodleCourseModuleSummaryViewModel> CoursesModulesItems { get; set; }

        #endregion
    }
}