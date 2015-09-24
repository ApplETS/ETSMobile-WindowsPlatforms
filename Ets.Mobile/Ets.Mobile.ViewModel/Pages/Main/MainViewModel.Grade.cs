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
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Akavache;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Comparators;
using Ets.Mobile.ViewModel.Content.Main;
using Ets.Mobile.ViewModel.Pages.Grade;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Presenter;
using Refit;
using Splat;
using StoreFramework.Controls.Presenter.Exceptions;
using StoreFramework.Messaging.Common;

namespace Ets.Mobile.ViewModel.Pages.Main
{
    public partial class MainViewModel
    {
        private void InitializeGrade()
        {
            NavigateToGradeItem = ReactiveCommand.CreateAsyncTask(param =>
            {
                var s = param as string;
                if (s != null)
                {
                    RxApp.MainThreadScheduler.Schedule(() =>
                    {
                        HostScreen.Router.Navigate.Execute(new GradeViewModel(HostScreen, s));
                    });
                }
                return Task.FromResult(Unit.Default);
            });

            GradesItems = new ReactiveList<GradeSummaryViewModelGroup>();

            LoadGrades = ReactiveCommand.CreateAsyncObservable(p =>
            {
                return Observable.Defer(() =>
                {
                    return Cache.GetAndFetchLatest(ViewModelKeys.Courses, async () =>
                    {
                        var courses = await ClientServices().SignetsService.Courses();
                        foreach(var course in courses.Where(x => x.Semester != "s.o.")
                                                     .GroupBy(x => x.Semester))
                        {
                            await SettingsService().ApplyColorOnItemsForSemester(
                                    courses.Where(x => x.Semester == course.FirstOrDefault().Semester).ToArray(),
                                    course.FirstOrDefault().Semester, x => x.Acronym);
                        }

                        return courses.Where(x => x.Semester != "s.o.").OrderByDescending(x => x.Semester, new SemestersComparator()).ToList();
                    })
                    .Select(courses => courses.GroupBy(course => course.Semester).Select(course => new GradeSummaryViewModelGroup(course.Key, course.ToList(), NavigateToGradeItem)).ToList());
                });
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
                            exceptionMessage.Content.Message = Locator.Current.GetService<ResourceLoader>().GetString("NetworkError");
                            exceptionMessage.Content.Title = Locator.Current.GetService<ResourceLoader>().GetString("NetworkTitleError");
                        }
                        exception = exceptionMessage;
                    }
                    else if (x is ReactivePresenterExceptionBase)
                    {
                        var exceptionMessage = new ErrorMessageContent(x.Message, x);
                        exception = exceptionMessage;
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

        private ReactiveList<GradeSummaryViewModelGroup> _gradesItems;
        [DataMember]
        public ReactiveList<GradeSummaryViewModelGroup> GradesItems
        {
            get { return _gradesItems; }
            set { this.RaiseAndSetIfChanged(ref _gradesItems, value); }
        }

        private ReactiveCommand<Unit> NavigateToGradeItem;

        [DataMember]
        public IReactiveDerivedList<GradeGroupViewModel> Grades { get; protected set; }
        public IReactivePresenterViewModel<ReactiveList<GradeSummaryViewModelGroup>> GradesPresenter { get; protected set; }
        public ReactiveCommand<List<GradeSummaryViewModelGroup>> LoadGrades { get; protected set; }
        private readonly ReplaySubject<Exception> _gradesExceptionSubject = new ReplaySubject<Exception>();

        #endregion

        #region Methods

        [DataContract]
        public class GradeGroupViewModel : ReactiveObject, IDisposable
        {
            [DataMember]
            public GradeSummaryViewModelGroup Model { get; protected set; }

            public GradeGroupViewModel(GradeSummaryViewModelGroup gradeSummaryViewModelGroup)
            {
                Model = gradeSummaryViewModelGroup;
            }

            public void Dispose()
            {
                Model?.Dispose();
            }
        }

        #endregion
    }
}
