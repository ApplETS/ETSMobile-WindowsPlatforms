using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Bases;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.Serialization;
using Windows.UI.Xaml.Media;
using Akavache;
using Ets.Mobile.ViewModel.Content.Schedule;
using Messaging.UniversalApp.Common;
using ReactiveUI.Extensions;
using ReactiveUI.Xaml.Controls.ViewModel;
using Refit;
using Syncfusion.UI.Xaml.Schedule;

namespace Ets.Mobile.ViewModel.Pages.Schedule
{
    [DataContract]
    public class ScheduleViewModel : PageViewModelBase
    {
        public ScheduleViewModel(IScreen screen) : base(screen, "Schedule")
        {
            OnViewModelCreation();
        }

        protected override sealed void OnViewModelCreation()
        {
            ScheduleItems = new ReactiveList<ScheduleAppointment>();

            LoadSchedule = ReactiveDeferedCommand.CreateAsyncObservable(() =>
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
                ScheduleItems.AddRange(x.Select(y => new ScheduleAppointment
                {
                    Subject = y.ActivityName,
                    StartTime = y.StartDate,
                    EndTime = y.EndDate,
                    Location = y.Location,
                    AppointmentBackground = y.Brush
                }));
            });

            SchedulePresenter = ReactivePresenterViewModel<ReactiveList<ScheduleAppointment>>.Create(ScheduleItems, LoadSchedule.IsExecuting, _scheduleExceptionSubject);
        }

        #region Properties

        [DataMember]
        public ReactiveList<ScheduleAppointment> ScheduleItems { get; protected set; }
        public IReactivePresenterViewModel<ReactiveList<ScheduleAppointment>> SchedulePresenter { get; protected set; }
        public ReactiveCommand<IEnumerable<ScheduleVm>> LoadSchedule { get; protected set; }
        private readonly ReplaySubject<Exception> _scheduleExceptionSubject = new ReplaySubject<Exception>();

        #endregion
    }
}
