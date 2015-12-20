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
using Messaging.Interfaces.Common;
using Messaging.UniversalApp.Common;
using ReactiveUI.Xaml.Controls.Extensions;
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
                    .ThrowIfEmpty()
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
                    var empty = x is IMessagingContent;
                    if (empty)
                    {
                        ViewServices().Popup.ShowMessage(Resources().GetString("ScheduleEmptyMessage"), Resources().GetString("ScheduleEmptyTitle"));
                    }

                    UserError.Throw(x.Message, x);
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

        #endregion
    }
}