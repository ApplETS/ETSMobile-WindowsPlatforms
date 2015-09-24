using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.Serialization;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Akavache;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Content.Grade;
using ReactiveUI;
using Refit;
using Splat;
using StoreFramework.Controls.Presenter.Exceptions;
using StoreFramework.Messaging.Common;
using ReactiveUI.Xaml.Controls;
using ReactiveUI.Xaml.Controls.Presenter;

namespace Ets.Mobile.ViewModel.Pages.Grade
{
    [DataContract]
    public class GradeViewModel : PageViewModelBase, IDisposable
    {
        #region IDisposable

        public void Dispose()
        {
            Semester = null;
        }

        #endregion

        public GradeViewModel(IScreen screen, string semester) 
            : base(screen, "Evaluation")
        {
            Semester = semester;
            OnViewModelCreation();
        }

        protected override sealed void OnViewModelCreation()
        {
            GradeItems = new ReactiveList<GradeViewModelItem>();

            LoadGrade = ReactiveCommand.CreateAsyncObservable(_ =>
            {
                return Observable.Defer(() =>
                {
                    return Cache.GetAndFetchLatest(ViewModelKeys.Semesters, () => ClientServices().SignetsService.Courses())
                        .Where(x => x != null && x.Any(y => !string.IsNullOrEmpty(y.Semester)) && x.Any(y => y.Semester == Semester))
                        .Select(x => x.Where(y => y.Semester == Semester))
                        .Select(x => x.Select(y => new GradeViewModelItem(y)));
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
                    _gradesExceptionSubject.OnNext(exception);
                });

            LoadGrade.Subscribe(x =>
            {
                GradeItems.Clear();
                GradeItems.AddRange(x);
            });

            Grades = GradeItems.CreateDerivedCollection(
                x => new GradeTileViewModel(x),
                x => x.Dispose()
            );
        }

        #region Properties

        public ReactiveCommand<IEnumerable<GradeViewModelItem>> LoadGrade { get; protected set; }
        private readonly ReplaySubject<Exception> _gradesExceptionSubject = new ReplaySubject<Exception>();


        private string _semester;
        [DataMember]
        public string Semester
        {
            get { return _semester; }
            set { this.RaiseAndSetIfChanged(ref _semester, value); }
        }

        private ReactiveList<GradeViewModelItem> _gradeItems;
        [DataMember]
        public ReactiveList<GradeViewModelItem> GradeItems
        {
            get { return _gradeItems; }
            set { this.RaiseAndSetIfChanged(ref _gradeItems, value); }
        }

        [DataMember]
        public IReactiveDerivedList<GradeTileViewModel> Grades { get; protected set; }

        #endregion

        #region Methods



        #endregion

        [DataContract]
        public class GradeTileViewModel : ReactiveObject, IDisposable
        {
            #region IDisposable

            public void Dispose()
            {
                Model.Dispose();
            }

            #endregion

            [DataMember]
            public GradeViewModelItem Model { get; protected set; }

            public GradeTileViewModel(GradeViewModelItem model)
            {
                Model = model;
            }
        }
    }
}
