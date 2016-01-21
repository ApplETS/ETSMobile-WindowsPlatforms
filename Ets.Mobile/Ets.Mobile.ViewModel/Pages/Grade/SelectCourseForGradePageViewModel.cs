using Akavache;
using Ets.Mobile.Client.Mixins;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Comparators;
using Ets.Mobile.ViewModel.Content.Main;
using Ets.Mobile.ViewModel.Contracts.Grade;
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

namespace Ets.Mobile.ViewModel.Pages.Grade
{
    public class SelectCourseForGradePageViewModel : ViewModelBase, ISelectCourseForGradeViewModel
    {
        public SelectCourseForGradePageViewModel(IScreen screen) : base(screen, "SelectCourse")
        {
            OnViewModelCreation();
        }

        protected sealed override void OnViewModelCreation()
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
                .Select(y => y.OrderByDescending(x => x.Semester, new SemestersComparator()).ToList())
                .Select(courses => courses.GroupBy(course => course.Semester).Select(course => new GradeSummaryViewModelGroup(course.Key, course.ToList(), _navigateToGradeItem)).ToList())
                .Publish();

            fetchCoursesAndSort.Connect();

            return fetchCoursesAndSort.ThrowIfEmpty();
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

        [DataMember]
        public ReactiveList<GradeSummaryViewModelGroup> GradesItems { get; protected set; }
        private ReactiveCommand<GradeSummaryViewModelItem> _navigateToGradeItem;
        public IReactiveDerivedList<GradeSummaryViewModelGroup> Grades { get; protected set; }
        public IReactivePresenterHandler<IReactiveDerivedList<GradeSummaryViewModelGroup>> GradesPresenter { get; protected set; }
        public ReactivePresenterCommand<List<GradeSummaryViewModelGroup>> LoadCoursesSummaries { get; protected set; }

        #endregion
    }
}