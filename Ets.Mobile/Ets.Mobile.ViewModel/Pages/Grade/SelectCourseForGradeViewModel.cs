using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Akavache;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Comparators;
using Ets.Mobile.ViewModel.Content.Main;
using ReactiveUI;
using Ets.Mobile.ViewModel.Contracts.Grade;
using Ets.Mobile.ViewModel.Mixins;
using Messaging.Interfaces.Popup;
using ReactiveUI.Xaml.Controls.Core;
using ReactiveUI.Xaml.Controls.Handlers;
using Splat;

namespace Ets.Mobile.ViewModel.Pages.Grade
{
    public class SelectCourseForGradeViewModel : PageViewModelBase, ISelectCourseForGradeViewModel
    {
        public SelectCourseForGradeViewModel(IScreen screen) : base(screen, "SelectCourse")
        {
            OnViewModelCreation();
        }

        protected sealed override void OnViewModelCreation()
        {
            _navigateToGradeItem = ReactiveCommand.CreateAsyncTask(param =>
            {
                var selectedItem = param as GradeSummaryViewModelItem;
                if (selectedItem != null)
                {
                    RxApp.MainThreadScheduler.Schedule(() =>
                    {
                        HostScreen.Router.Navigate.Execute(new GradeViewModel(HostScreen, selectedItem.Course));
                    });
                }
                return Task.FromResult(Unit.Default);
            });

            GradesItems = new ReactiveList<GradeSummaryViewModelGroup>();

            LoadGrades = ReactivePresenterCommand.CreateAsyncObservable(_ => {
                return Cache.GetAndFetchLatest(ViewModelKeys.Courses, () =>
                    ClientServices().SignetsService.Courses()
                    .ToObservable()
                    .Select(x => x.Where(y => y.Semester != "s.o.").OrderByDescending(y => y.Semester, new SemestersComparator()))
                    .Do(async courses =>
                    {
                        foreach (var course in courses.Where(x => x.Semester != "s.o.")
                                                    .GroupBy(x => x.Semester))
                        {
                            await SettingsService().ApplyColorOnItemsForSemester(
                                    courses.Where(x => x.Semester == course.FirstOrDefault().Semester).ToArray(),
                                    course.FirstOrDefault().Semester, x => x.Acronym);
                        }
                    })
                    .Select(x => x.ToArray())
                )
                .Select(y => y.OrderByDescending(x => x.Semester, new SemestersComparator()).ToList())
                .Select(courses => courses.GroupBy(course => course.Semester).Select(course => new GradeSummaryViewModelGroup(course.Key, course.ToList(), _navigateToGradeItem)).ToList());
            });

            LoadGrades.ThrownExceptions
                .Subscribe(x =>
                {
                    UserError.Throw(x.Message, x);
                    x.HandleOfflineConnection(ViewServices().Notification);
                });

            Grades = GradesItems.CreateDerivedCollection(x => x, x => x.Dispose(),
                orderer: (x, y) => SemestersComparatorMethod.ReversedCompare(x.Semester, y.Semester));

            GradesPresenter = LoadGrades.CreateReactivePresenter(GradesItems, Grades, true);
        }

        #region Properties

        [DataMember]
        public ReactiveList<GradeSummaryViewModelGroup> GradesItems { get; protected set; }
        private ReactiveCommand<Unit> _navigateToGradeItem;
        public IReactiveDerivedList<GradeSummaryViewModelGroup> Grades { get; protected set; }
        public IReactivePresenterHandler<IReactiveDerivedList<GradeSummaryViewModelGroup>> GradesPresenter { get; protected set; }
        public ReactivePresenterCommand<List<GradeSummaryViewModelGroup>> LoadGrades { get; protected set; }

        #endregion
    }
}