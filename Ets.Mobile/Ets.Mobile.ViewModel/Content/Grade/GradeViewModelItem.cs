using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Text;
using Akavache;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Bases;
using ReactiveUI;
using System.Diagnostics;
using ReactiveUI.Xaml.Controls.Presenter;
using Refit;
using StoreFramework.Messaging.Common;
using Splat;
using Windows.ApplicationModel.Resources;
using StoreFramework.Controls.Presenter.Exceptions;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using ReactiveUI.Xaml.Controls;

namespace Ets.Mobile.ViewModel.Content.Grade
{
    [DataContract]
    public class GradeViewModelItem : ApplicationViewModelBase, IDisposable
    {
        #region IDisposable

        public void Dispose()
        {
        }

        #endregion

        private CourseVm _course;
        [DataMember] public CourseVm Course
        {
            get { return _course; }
            set { this.RaiseAndSetIfChanged(ref _course, value); }
        }

        private EvaluationsVm _evaluation;
        [DataMember] public EvaluationsVm Evaluation
        {
            get { return _evaluation; }
            set { this.RaiseAndSetIfChanged(ref _evaluation, value); }
        }
        
        public IReactivePresenterViewModel<EvaluationsVm> GradesPresenter { get; protected set; }
        public ReplaySubject<Exception> GradesExceptionSubject = new ReplaySubject<Exception>();
        public ReactiveCommand<EvaluationsVm> LoadGrade { get; protected set; }
        public bool HasTriggeredLoadGradeOnce { get; set; }
        
        public GradeViewModelItem(CourseVm course)
        {
            Course = course;

            HasTriggeredLoadGradeOnce = false;

            OnViewModelCreation();
        }

        protected override sealed void OnViewModelCreation()
        {
            LoadGrade = ReactiveCommand.CreateAsyncObservable(_ =>
            {
                return Observable.Defer(() =>
                {
                    return Cache.GetAndFetchLatest(ViewModelKeys.GradesForSemesterAndCourse(Course.Semester, Course.Name),
                        () => ClientServices().SignetsService.Evaluations(Course.Acronym, Course.Group, Course.Semester)
                            .ToObservable()
                            .Do(async grade => {
                                grade.LetterGrade = Course.Grade;
                                await SettingsService().ApplyColorOnItemsForSemester(grade, Course.Semester, x => Course.Acronym, Color.FromArgb(Course.A, Course.R, Course.G, Course.B));
                            })
                    );
                });
            });
            
            LoadGrade.ThrownExceptions
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
                    GradesExceptionSubject.OnNext(exception);
                });

            LoadGrade.Subscribe(e => Evaluation = e);

            GradesPresenter = ReactivePresenterViewModel<EvaluationsVm>.Create(LoadGrade, LoadGrade.IsExecuting, LoadGrade.IsEmpty(), GradesExceptionSubject);
        }
    }
}
