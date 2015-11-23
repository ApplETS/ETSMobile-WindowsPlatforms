using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Bases;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.Serialization;
using Akavache;
using Messaging.UniversalApp.Common;
using Refit;

namespace Ets.Mobile.ViewModel.Pages.Schedule
{
    [DataContract]
    public class ScheduleViewModel : ViewModelBase
    {
        public ScheduleViewModel(IScreen screen = null) : base(screen, "Schedule")
        {
            OnViewModelCreation();
        }

        protected sealed override void OnViewModelCreation()
        {
            ScheduleItems = new ReactiveList<ScheduleVm>();

            LoadSchedule = ReactiveCommand.CreateAsyncObservable(_ =>
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
                    .Select(x => x.AsEnumerable());
            });

            LoadSchedule.ThrownExceptions
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
                    _scheduleExceptionSubject.OnNext(exception);
                });

            LoadSchedule.Subscribe(x =>
            {
                ScheduleItems.Clear();
                ScheduleItems.AddRange(x);
            });
        }

        #region Properties

        [DataMember]
        public ReactiveList<ScheduleVm> ScheduleItems { get; protected set; }
        public ReactiveCommand<IEnumerable<ScheduleVm>> LoadSchedule { get; protected set; }
        private readonly ReplaySubject<Exception> _scheduleExceptionSubject = new ReplaySubject<Exception>();

        #endregion
    }
}