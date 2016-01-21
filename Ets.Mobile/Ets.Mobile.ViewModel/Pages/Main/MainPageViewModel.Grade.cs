using Akavache;
using Ets.Mobile.Client.Mixins;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Comparators;
using Ets.Mobile.ViewModel.Content.Main;
using Ets.Mobile.ViewModel.Pages.Grade;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;
using ReactiveUI.Xaml.Controls.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Ets.Mobile.ViewModel.Pages.Main
{
    public partial class MainPageViewModel
    {
        private void InitializeGrade()
        {
            _navigateToGradeItem = ReactiveCommand.CreateAsyncObservable(NavigateToGradeItemImpl);
            _navigateToGradeItem.Subscribe(selectedItem => HostScreen.Router.Navigate.Execute(new GradePageViewModel(HostScreen, selectedItem.Course)));

            GradesItems = new ReactiveList<GradeSummaryViewModelGroup>();

            LoadCoursesSummaries = ReactivePresenterCommand.CreateAsyncObservable(_ => FetchCoursesWithSummariesImpl());

            LoadCoursesSummaries.ThrownExceptions
                .Subscribe(x =>
                {
                    UserError.Throw(x.Message, x);
                });

            Grades = GradesItems.CreateDerivedCollection(x => x, x => x.Dispose(),
                orderer: (x, y) => SemestersComparatorMethod.ReversedCompare(x.Semester, y.Semester));

            GradesPresenter = LoadCoursesSummaries.CreateReactivePresenter(GradesItems, Grades, true);
        }

        private IObservable<List<GradeSummaryViewModelGroup>> FetchCoursesWithSummariesImpl()
        {
            var fetchCoursesAndSort = Cache.GetAndFetchLatest(ViewModelKeys.Courses, FetchCourses)
                .Select(y => y.OrderByDescending(x => x.Semester, new SemestersComparator()).ToList());

            var createCoursesSummaries =
                fetchCoursesAndSort
                .Select(courses => courses.GroupBy(course => course.Semester).Select(course => new GradeSummaryViewModelGroup(course.Key, course.ToList(), _navigateToGradeItem)).ToList());

            return createCoursesSummaries;
        }

        private Task<CourseVm[]> FetchCourses()
        {
            return ClientServices().SignetsService.Courses().ApplyCustomColors(SettingsService());
        }

        private IObservable<GradeSummaryViewModelItem> NavigateToGradeItemImpl(object param)
        {
            var selectedItem = param as GradeSummaryViewModelItem;
            if (selectedItem != null)
            {
                return Observable.Return(selectedItem);
            }
            return Observable.Empty<GradeSummaryViewModelItem>();
        }

        #region Properties

        private ReactiveCommand<GradeSummaryViewModelItem> _navigateToGradeItem;
        [DataMember] public ReactiveList<GradeSummaryViewModelGroup> GradesItems { get; set; }
        [DataMember] public IReactiveDerivedList<GradeSummaryViewModelGroup> Grades { get; set; }
        public IReactivePresenterHandler<IReactiveDerivedList<GradeSummaryViewModelGroup>> GradesPresenter { get; protected set; }
        public ReactivePresenterCommand<List<GradeSummaryViewModelGroup>> LoadCoursesSummaries { get; protected set; }

        #endregion
    }
}