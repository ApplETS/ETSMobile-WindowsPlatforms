﻿using Akavache;
using Ets.Mobile.Client.Mixins;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Contracts.Schedule;
using Messaging.Interfaces.Common;
using ReactiveUI;
using ReactiveUI.Extensions;
using ReactiveUI.Xaml.Controls.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.Serialization;

namespace Ets.Mobile.ViewModel.Pages.Schedule
{
    [DataContract]
    public class SchedulePageViewModel : ViewModelBase, ISchedulePageViewModel
    {
        public SchedulePageViewModel(IScreen screen = null) : base(screen, "Schedule")
        {
            OnViewModelCreation();
        }

        protected sealed override void OnViewModelCreation()
        {
            ScheduleItems = new ReactiveList<ScheduleVm>();

            FetchSchedule = ReactiveCommand.CreateAsyncObservable(_ => FetchScheduleImpl());

            FetchSchedule.ThrownExceptions
                .Subscribe(x =>
                {
                    var empty = x is IMessagingContent;
                    if (empty)
                    {
                        ViewServices().Popup.ShowMessage(Resources().GetStringForKey("ScheduleEmptyMessage"), Resources().GetStringForKey("ScheduleEmptyTitle"));
                    }

                    UserError.Throw(x.Message, x);
                });

            FetchSchedule.Subscribe(scheduleVms =>
            {
                ScheduleItems.MergeWith(scheduleVms);
            });
        }

        public IObservable<IEnumerable<ScheduleVm>> FetchScheduleImpl()
        {
            var fetchSchedule =
                Cache.GetAndFetchLatest(ViewModelKeys.Semesters, () => ClientServices().SignetsService.Semesters())
                    .Where(x => x != null && x.Any(y => !string.IsNullOrEmpty(y.AbridgedName)))
                    .SelectMany(x => x)
                    .Where(x => (x.StartDate <= DateTime.Now && x.EndDate > DateTime.Now) || (x.StartDate > DateTime.Now))
                    .SelectMany(currentSemester =>
                        Cache.GetAndFetchLatest(ViewModelKeys.ScheduleForSemester(currentSemester.AbridgedName), async () => await ClientServices().SignetsService.Schedule(currentSemester.AbridgedName).ApplyCustomColors(SettingsService()))
                    )
                    .Where(x => x != null)
                    .Select(x => x.AsEnumerable())
                    .Publish();

            fetchSchedule.Connect();

            return fetchSchedule.ThrowIfEmpty();
        }

        #region Properties

        [DataMember]
        public ReactiveList<ScheduleVm> ScheduleItems { get; protected set; }
        public ReactiveCommand<IEnumerable<ScheduleVm>> FetchSchedule { get; protected set; }

        #endregion
    }
}