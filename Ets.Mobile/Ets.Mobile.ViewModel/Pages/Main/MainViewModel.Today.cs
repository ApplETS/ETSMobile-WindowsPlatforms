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
            TodayItems = new ReactiveList<ScheduleVm>();

            LoadCoursesForToday = ReactiveCommand.CreateAsyncObservable(_ =>
            {
                return Observable.Defer(() =>
                {
                    return Cache.GetAndFetchLatest(ViewModelKeys.Semesters, () => ClientServices().SignetsService.Semesters())
                        .Where(x => x != null && x.Any(y => !string.IsNullOrEmpty(y.AbridgedName)))
                        .SelectMany(x => x)
                        .FirstAsync(x => x.StartDate <= DateTime.Now && x.EndDate > DateTime.Now)
                        .SelectMany(currentSemester => Cache.GetAndFetchLatest(ViewModelKeys.ScheduleForSemester(currentSemester.AbridgedName), async () => {
                            var schedule = await ClientServices().SignetsService.Schedule(currentSemester.AbridgedName);
                            await SettingsService().ApplyColorOnItemsForSemester(schedule, currentSemester.AbridgedName, x => x.Title);
                            return schedule;
                        }))
                        .Where(x => x != null)
                        .Select(x => x);
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
                x => new ScheduleTileViewModel(x),
                x => x.Dispose(),
                x => x.StartDate.Date.Equals(DateTime.Now.Date),
                (x, y) => TimeSpan.Compare(x.Model.StartDate.TimeOfDay, y.Model.StartDate.TimeOfDay));

            TodayPresenter = ReactivePresenterViewModel<ReactiveList<ScheduleVm>>.Create(TodayItems, Today, LoadCoursesForToday.IsExecuting, _scheduleExceptionSubject);
        }

        #region Properties

        [DataMember] public ReactiveList<ScheduleVm> TodayItems { get; protected set; }
        [DataMember] public IReactiveDerivedList<ScheduleTileViewModel> Today { get; protected set; }
        public IReactivePresenterViewModel<ReactiveList<ScheduleVm>> TodayPresenter { get; protected set; }
        public ReactiveCommand<ScheduleVm[]> LoadCoursesForToday { get; protected set; }
        private readonly ReplaySubject<Exception> _scheduleExceptionSubject = new ReplaySubject<Exception>();

        #endregion

        #region Methods

        [DataContract]
        public class ScheduleTileViewModel : ReactiveObject, IDisposable
        {
            [DataMember]
            public ScheduleVm Model { get; protected set; }

            public ScheduleTileViewModel(ScheduleVm model)
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

                timer.Where(x => Model.StartDate.TimeOfDay > DateTime.Now.TimeOfDay && (Model.StartDate.TimeOfDay - DateTime.Now.TimeOfDay).TotalMinutes > 0 && (Model.StartDate.TimeOfDay - DateTime.Now.TimeOfDay).TotalMinutes < 60)
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(x =>
                    {
                        TimeRemaining = (Model.StartDate.TimeOfDay.Minutes - DateTime.Now.TimeOfDay.Minutes).ToString();
                        IsTimeRemainingVisible = true;
                    })
                    .DisposeWith(_timeRemainingDisposable);

                timer.Where(x => Model.StartDate.TimeOfDay < DateTime.Now.TimeOfDay && (Model.StartDate.TimeOfDay - DateTime.Now.TimeOfDay).Minutes < 0 || (Model.StartDate.TimeOfDay - DateTime.Now.TimeOfDay).Minutes > 60)
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
