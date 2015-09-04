using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Runtime.Serialization;
using Windows.ApplicationModel.Resources;
using Akavache;
using Ets.Mobile.Entities.Signets;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Presenter;
using Refit;
using Splat;
using StoreFramework.Controls.Presenter.Exceptions;
using StoreFramework.Extensions;
using StoreFramework.Messaging.Common;

namespace Ets.Mobile.ViewModel.Pages.Main
{
    public partial class MainViewModel
    {
        private void InitializeToday()
        {
            TodayItems = new ReactiveList<ActivityVm>();

            LoadCoursesForToday = ReactiveCommand.CreateAsyncObservable(_ =>
            {
                return Observable.Defer(() =>
                {
                    return Cache.GetAndFetchLatest(ViewModelKeys.Semesters, () => ClientServices().SignetsService.Semesters())
                        .Where(x => x != null && x.Any(y => !string.IsNullOrEmpty(y.AbridgedName)))
                        .SelectMany(x => x)
                        .FirstAsync(x => x.StartDate <= DateTime.Now && x.EndDate > DateTime.Now)
                        .SelectMany(currentSemester => Cache.GetAndFetchLatest(ViewModelKeys.ScheduleForSemester(currentSemester.AbridgedName), async () => {
                            var schedule = await ClientServices().SignetsService.ScheduleAndTeachers(currentSemester.AbridgedName);
                            await SettingsService().ApplyColorOnCoursesForSemester(schedule.Activities, currentSemester.AbridgedName, x => x.Acronym);
                            return schedule;
                        }))
                        .Where(x => x?.Activities != null)
                        .Select(x => x.Activities);
                });
            });

            LoadCoursesForToday.ThrownExceptions
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
                    _scheduleExceptionSubject.OnNext(exception);
                });

            LoadCoursesForToday.Subscribe(x =>
            {
                TodayItems.Clear();
                TodayItems.AddRange(x);
            });

            Today = TodayItems.CreateDerivedCollection(
                x => new ActivityTileViewModel(x),
                x => x.Dispose(),
                x => x.Day == (int)DateTime.Now.DayOfWeek,
                (x, y) => TimeSpan.Compare(x.Model.StartHour, y.Model.StartHour));

            TodayPresenter = ReactivePresenterViewModel<ReactiveList<ActivityVm>>.Create(TodayItems, Today, LoadCoursesForToday.IsExecuting, _scheduleExceptionSubject);
        }

        #region Properties

        [DataMember] public ReactiveList<ActivityVm> TodayItems { get; protected set; }
        [DataMember] public IReactiveDerivedList<ActivityTileViewModel> Today { get; protected set; }
        public IReactivePresenterViewModel<ReactiveList<ActivityVm>> TodayPresenter { get; protected set; }
        public ReactiveCommand<List<ActivityVm>> LoadCoursesForToday { get; protected set; }
        private readonly ReplaySubject<Exception> _scheduleExceptionSubject = new ReplaySubject<Exception>();

        #endregion

        #region Methods

        [DataContract]
        public class ActivityTileViewModel : ReactiveObject, IDisposable
        {
            [DataMember]
            public ActivityVm Model { get; protected set; }

            public ActivityTileViewModel(ActivityVm model)
            {
                Model = model;
                _timeRemainingDisposable = new CompositeDisposable();
                IsTimeRemainingVisible = false;
                //BindTimeRemaining();
            }

            private readonly CompositeDisposable _timeRemainingDisposable;

            #region IDisposable Implementation

            public void Dispose()
            {
                _timeRemainingDisposable?.Dispose();
            }

            #endregion

            #region Time Remaining

            private string _timeRemaining;
            public string TimeRemaining
            {
                get { return _timeRemaining; }
                set { this.RaiseAndSetIfChanged(ref _timeRemaining, value); }
            }

            private bool _isTimeRemainingVisible;
            public bool IsTimeRemainingVisible
            {
                get { return _isTimeRemainingVisible; }
                set { this.RaiseAndSetIfChanged(ref _isTimeRemainingVisible, value); }
            }

            public void BindTimeRemaining()
            {
                var timer = Observable.Timer(TimeSpan.Zero, TimeSpan.FromMinutes(1));

                timer.Where(x => Model.StartHour > DateTime.Now.TimeOfDay && (Model.StartHour - DateTime.Now.TimeOfDay).TotalMinutes > 0 && (Model.StartHour - DateTime.Now.TimeOfDay).TotalMinutes < 60)
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(x =>
                    {
                        TimeRemaining = (Model.StartHour.Minutes - DateTime.Now.TimeOfDay.Minutes).ToString();
                        IsTimeRemainingVisible = true;
                    })
                    .DisposeWith(_timeRemainingDisposable);

                timer.Where(x => Model.StartHour < DateTime.Now.TimeOfDay && (Model.StartHour - DateTime.Now.TimeOfDay).Minutes < 0 || (Model.StartHour - DateTime.Now.TimeOfDay).Minutes > 60)
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(x =>
                    {
                        if (IsTimeRemainingVisible)
                        {
                            IsTimeRemainingVisible = false;
                        }
                    })
                    .DisposeWith(_timeRemainingDisposable);
            }

            #endregion
        }

        #endregion
    }
}
