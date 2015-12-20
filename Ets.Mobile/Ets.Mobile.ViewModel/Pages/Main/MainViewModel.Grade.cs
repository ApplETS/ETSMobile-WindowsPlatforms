using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Akavache;
using Ets.Mobile.ViewModel.Comparators;
using Ets.Mobile.ViewModel.Content.Main;
using Ets.Mobile.ViewModel.Pages.Grade;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;
using ReactiveUI.Xaml.Controls.Handlers;

namespace Ets.Mobile.ViewModel.Pages.Main
{
    public partial class MainViewModel
    {
        private void InitializeGrade()
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
                return Cache.GetAndFetchLatest(ViewModelKeys.Courses, async () =>
                {
                    var courses = await ClientServices().SignetsService.Courses();
                    courses = courses.OrderByDescending(y => y.Semester, new SemestersComparator()).ToArray();
                    foreach (var course in courses.GroupBy(x => x.Semester))
                    {
                        await SettingsService().ApplyColorOnItemsForSemester(
                                courses.Where(x => x.Semester == course.FirstOrDefault().Semester).ToArray(),
                                course.FirstOrDefault().Semester, x => x.Acronym);
                    }
                    return courses;
                })
                .Select(courses => courses.GroupBy(course => course.Semester).Select(course => new GradeSummaryViewModelGroup(course.Key, course.ToList(), _navigateToGradeItem)));
            });

            LoadGrades.ThrownExceptions
                .Subscribe(x =>
                {
                    UserError.Throw(x.Message, x);
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
        public ReactivePresenterCommand<IEnumerable<GradeSummaryViewModelGroup>> LoadGrades { get; protected set; }

        #endregion
    }
}