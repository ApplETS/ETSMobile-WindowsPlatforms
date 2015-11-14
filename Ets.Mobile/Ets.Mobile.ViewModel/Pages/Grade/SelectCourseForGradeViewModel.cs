using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Akavache;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Comparators;
using Ets.Mobile.ViewModel.Content.Main;
using Messaging.UniversalApp.Common;
using ReactiveUI;
using ReactiveUI.Extensions;
using ReactiveUI.Xaml.Controls.ViewModel;
using Refit;
using Ets.Mobile.ViewModel.Contracts.Grade;

namespace Ets.Mobile.ViewModel.Pages.Grade
{
    public class SelectCourseForGradeViewModel : PageViewModelBase, ISelectCourseForGradeViewModel
    {
        public SelectCourseForGradeViewModel(IScreen screen) : base(screen, "SelectCourse")
        {
            OnViewModelCreation();
        }

        protected override sealed void OnViewModelCreation()
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

            LoadGrades = ReactiveDeferedCommand.CreateAsyncObservable(() =>
            {
                return Cache.GetAndFetchLatest(ViewModelKeys.Courses, async () =>
                {
                    var courses = await ClientServices().SignetsService.Courses();
                    foreach (var course in courses.Where(x => x.Semester != "s.o.")
                                                    .GroupBy(x => x.Semester))
                    {
                        await SettingsService().ApplyColorOnItemsForSemester(
                                courses.Where(x => x.Semester == course.FirstOrDefault().Semester).ToArray(),
                                course.FirstOrDefault().Semester, x => x.Acronym);
                    }

                    return courses.Where(x => x.Semester != "s.o.").OrderByDescending(x => x.Semester, new SemestersComparator()).ToList();
                })
                .Select(courses => courses.GroupBy(course => course.Semester).Select(course => new GradeSummaryViewModelGroup(course.Key, course.ToList(), _navigateToGradeItem)).ToList());
            });

            LoadGrades.ThrownExceptions
                .Subscribe(x =>
                {
                    UserError.Throw(x.Message, x);
                    Exception exception;
                    var apiException = x as ApiException;
                    if (apiException != null)
                    {
                        var exceptionMessage = new ErrorMessageContent(x.Message, apiException);
                        if (apiException.ReasonPhrase == "Not Found")
                        {
                            exceptionMessage.Message = Resources().GetString("NetworkError");
                            exceptionMessage.Title = Resources().GetString("NetworkTitleError");
                        }
                        exception = exceptionMessage.Exception;
                    }
                    else
                    {
                        exception = x;
                    }
                    _gradesExceptionSubject.OnNext(exception);
                });

            LoadGrades
                .Subscribe(x =>
                {
                    GradesItems.Clear();
                    GradesItems.AddRange(x);
                });

            Grades = GradesItems.CreateDerivedCollection(x => new GradeGroupViewModel(x), x => x.Dispose(),
                orderer: (x, y) => SemestersComparatorMethod.ReversedCompare(x.Model.Semester, y.Model.Semester));

            GradesPresenter = ReactivePresenterViewModel<ReactiveList<GradeSummaryViewModelGroup>>.Create(GradesItems, Grades, LoadGrades.IsExecuting, _gradesExceptionSubject);
        }

        #region Properties

        [DataMember]
        public ReactiveList<GradeSummaryViewModelGroup> GradesItems { get; protected set; }
        private ReactiveCommand<Unit> _navigateToGradeItem;
        public IReactiveDerivedList<GradeGroupViewModel> Grades { get; protected set; }
        public IReactivePresenterViewModel<ReactiveList<GradeSummaryViewModelGroup>> GradesPresenter { get; protected set; }
        public ReactiveCommand<List<GradeSummaryViewModelGroup>> LoadGrades { get; protected set; }
        private readonly ReplaySubject<Exception> _gradesExceptionSubject = new ReplaySubject<Exception>();

        #endregion
    }
}
