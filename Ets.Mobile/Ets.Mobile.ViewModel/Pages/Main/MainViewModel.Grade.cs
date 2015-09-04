using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Runtime.Serialization;
using Windows.ApplicationModel.Resources;
using Akavache;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Comparators;
using Ets.Mobile.ViewModel.Content.Main;
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
            GradesItems = new ReactiveList<GradeViewModelGroup>();

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
                            await SettingsService().ApplyColorOnCoursesForSemester(
                                    courses.Where(x => x.Semester == course.FirstOrDefault().Semester).ToArray(),
                                    course.FirstOrDefault().Semester, x => x.Acronym);
                        }

                        return courses.Where(x => x.Semester != "s.o.").OrderByDescending(x => x.Semester, new SemestersComparator()).ToList();
                    })
                    .Select(courses => courses.GroupBy(course => course.Semester).Select(course => new GradeViewModelGroup(course.Key, course.ToList())).ToList());
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

            GradesPresenter = ReactivePresenterViewModel<ReactiveList<GradeViewModelGroup>>.Create(GradesItems, Grades, LoadGrades.IsExecuting, _gradesExceptionSubject);
        }

        #region Properties

        private ReactiveList<GradeViewModelGroup> _gradesItems;
        [DataMember]
        public ReactiveList<GradeViewModelGroup> GradesItems
        {
            get { return _gradesItems; }
            set { this.RaiseAndSetIfChanged(ref _gradesItems, value); }
        }

        [DataMember]
        public IReactiveDerivedList<GradeGroupViewModel> Grades { get; protected set; }
        public IReactivePresenterViewModel<ReactiveList<GradeViewModelGroup>> GradesPresenter { get; protected set; }
        public ReactiveCommand<List<GradeViewModelGroup>> LoadGrades { get; protected set; }
        private readonly ReplaySubject<Exception> _gradesExceptionSubject = new ReplaySubject<Exception>();

        #endregion

        #region Methods

        [DataContract]
        public class GradeGroupViewModel : ReactiveObject, IDisposable
        {
            [DataMember]
            public GradeViewModelGroup Model { get; protected set; }

            public GradeGroupViewModel(GradeViewModelGroup gradeViewModelGroup)
            {
                Model = gradeViewModelGroup;
            }

            public void Dispose()
            {
                Model?.Dispose();
            }
        }

        #endregion
    }
}
