using Akavache;
using Ets.Mobile.Client.Mixins;
using Ets.Mobile.Entities.Moodle;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Content.Moodle.Courses.Content;
using Ets.Mobile.ViewModel.Contracts.Moodle.Courses;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;
using ReactiveUI.Xaml.Controls.Extensions;
using ReactiveUI.Xaml.Controls.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Ets.Mobile.ViewModel.Pages.Moodle.Courses
{
    [DataContract]
    public class MoodleCourseModulePageViewModel : ViewModelBase, IMoodleCourseModulePageViewModel
    {
        public MoodleCourseModulePageViewModel(IScreen screen, MoodleCourseVm course, MoodleCourseContentVm content) : base(screen, "MoodleCourseModule")
        {
            Course = course;
            CourseContent = content;
            OnViewModelCreation();
        }

        protected sealed override void OnViewModelCreation()
        {
            _navigateToModuleItem = ReactiveCommand.CreateAsyncObservable(NavigateToModuleItemImpl);
            _navigateToModuleItem.Subscribe(selectedItem => HostScreen.Router.Navigate.Execute(selectedItem));

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

        private IObservable<MoodleCourseModuleContentPageViewModel> NavigateToModuleItemImpl(object param)
        {
            var selectedItem = param as MoodleCourseModuleSummaryViewModel;
            if (selectedItem != null)
            {
                if (!string.IsNullOrEmpty(selectedItem.CourseModule.Url))
                {
                    return Observable.Return(new MoodleCourseModuleContentPageViewModel(HostScreen, selectedItem.Course, selectedItem.CourseContent, selectedItem.CourseModule));
                }
            }
            return Observable.Empty<MoodleCourseModuleContentPageViewModel>();
        }

        private IObservable<List<MoodleCourseModuleSummaryViewModel>> FetchCoursesModulesImpl()
        {
            var fetchCoursesContent = Cache.GetAndFetchLatest(ViewModelKeys.MoodleCoursesContentForCourse(Course.Id), FetchMoodleCoursesContents)
                .Where(s => s.Any(sc => sc.Id == CourseContent.Id && sc.Modules.Length > 0))
                .Select(sc => sc.First(s => s.Id == CourseContent.Id).Modules.Select(m => new MoodleCourseModuleSummaryViewModel(Course, CourseContent, m, _navigateToModuleItem)).ToList())
                .Publish();

            fetchCoursesContent.Connect();

            return fetchCoursesContent.ThrowIfEmpty();
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

        private ReactiveCommand<MoodleCourseModuleContentPageViewModel> _navigateToModuleItem;
        public IReactiveDerivedList<MoodleCourseModuleSummaryViewModel> CoursesModules { get; protected set; }
        public ReactivePresenterCommand<List<MoodleCourseModuleSummaryViewModel>> LoadCoursesModule { get; set; }
        public IReactivePresenterHandler<IReactiveDerivedList<MoodleCourseModuleSummaryViewModel>> CoursesModulePresenter { get; set; }
        public ReactiveList<MoodleCourseModuleSummaryViewModel> CoursesModulesItems { get; set; }

        #endregion
    }
}