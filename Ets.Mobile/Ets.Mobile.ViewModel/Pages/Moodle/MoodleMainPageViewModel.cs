using Akavache;
using Ets.Mobile.Client.Mixins;
using Ets.Mobile.Entities.Moodle;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Comparators;
using Ets.Mobile.ViewModel.Content.Moodle.Courses;
using Ets.Mobile.ViewModel.Pages.Moodle.Courses;
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
using System.Threading.Tasks;

namespace Ets.Mobile.ViewModel.Pages.Moodle
{
    public class MoodleMainPageViewModel : ViewModelBase
    {
        public MoodleMainPageViewModel(IScreen screen) : base(screen, "MoodleMainPage")
        {
            OnViewModelCreation();
        }

        protected sealed override void OnViewModelCreation()
        {
            _navigateToCourseItem = ReactiveCommand.CreateAsyncTask(NavigateToCourseItemImpl);

            CoursesItems = new ReactiveList<MoodleCourseSummaryViewModelGroup>();

            Courses = CoursesItems.CreateDerivedCollection(x => x, x => x.Dispose(),
                orderer: (x, y) => SemestersComparatorMethod.ReversedCompare(x.Semester, y.Semester));

            LoadCourses = ReactivePresenterCommand.CreateAsyncObservable(_ => FetchMoodleCoursesImpl());

            LoadCourses.ThrownExceptions
                .Subscribe(x =>
                {
                    UserError.Throw(x.Message, x);
                });

            CoursesPresenter = LoadCourses.CreateReactivePresenter(CoursesItems, Courses, true);
        }

        private Task<Unit> NavigateToCourseItemImpl(object param)
        {
            var selectedItem = param as MoodleCourseSummaryViewModelItem;
            if (selectedItem != null)
            {
                RxApp.MainThreadScheduler.Schedule(() =>
                {
                    HostScreen.Router.Navigate.Execute(new MoodleCourseContentPageViewModel(HostScreen, selectedItem.Course));
                });
            }
            return Task.FromResult(Unit.Default);
        }

        private IObservable<List<MoodleCourseSummaryViewModelGroup>> FetchMoodleCoursesImpl()
        {
            var fetchCourses = Cache.GetAndFetchLatest(ViewModelKeys.MoodleCourses, FetchMoodleCourses);

            var createSummaries = fetchCourses
                    .Select(sc => sc.GroupBy(scg => scg.Semester).Select(s => new MoodleCourseSummaryViewModelGroup(s.Key, s.AsEnumerable(), _navigateToCourseItem)).ToList());
                    
            return createSummaries.ThrowIfEmpty();
        }

        private Task<MoodleCourseVm[]> FetchMoodleCourses()
        {
            return ClientServices().MoodleService.Courses().ApplyCustomColors(SettingsService());
        }

        #region Properties

        private ReactiveCommand<Unit> _navigateToCourseItem;
        public IReactiveDerivedList<MoodleCourseSummaryViewModelGroup> Courses { get; protected set; }
        public ReactivePresenterCommand<List<MoodleCourseSummaryViewModelGroup>> LoadCourses { get; set; }
        public IReactivePresenterHandler<IReactiveDerivedList<MoodleCourseSummaryViewModelGroup>> CoursesPresenter { get; set; }
        public ReactiveList<MoodleCourseSummaryViewModelGroup> CoursesItems { get; set; }

        #endregion
    }
}