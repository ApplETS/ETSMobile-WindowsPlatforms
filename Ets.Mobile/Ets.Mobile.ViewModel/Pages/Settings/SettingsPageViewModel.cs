﻿using Akavache;
using Ets.Mobile.Client.Mixins;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Contracts.Settings;
using Ets.Mobile.ViewModel.Helpers;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;
using ReactiveUI.Xaml.Controls.Extensions;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.ApplicationModel;
#if WINDOWS_PHONE_APP || WINDOWS_UWP
#endif

namespace Ets.Mobile.ViewModel.Pages.Settings
{
    public class SettingsPageViewModel : ViewModelBase, ISettingsPageViewModel
    {
        public SettingsPageViewModel(IScreen screen) : base(screen, "Settings")
        {
            OnViewModelCreation();
        }

        protected sealed override void OnViewModelCreation()
        {
            IsScheduleBackgroundServiceActive = true;
            
            SendFeedbackUri = new Uri("mailto:applets@ens.etsmtl.ca?subject=" +
#if WINDOWS_PHONE_APP
            "ÉtsMobile-WindowsPhone"
#elif WINDOWS_APP
            "ÉtsMobile-Windows"
#elif WINDOWS_UWP
            "ÉtsMobile-UWP"
#endif
            );

            SendLogFiles = ReactiveCommand.CreateAsyncTask(async _ => await SendLogFilesImpl());

            SendLogFiles.ThrownExceptions.Subscribe(ex =>
            {
                UserError.Throw(ex.Message, ex);
            });

            HandleScheduleBackgroundService = ReactiveCommand.CreateAsyncTask(async b =>
            {
                var registerScheduleBgService = b as bool?;
                if (registerScheduleBgService.HasValue && registerScheduleBgService.Value)
                {
                    await Agent.ScheduleTileUpdaterBackgroundTask.Register();
                    await Cache.InsertObject(ViewModelKeys.ScheduleTileUpdaterActive, true).ToTask();
                    return true;
                }
                else
                {
                    await Agent.ScheduleTileUpdaterBackgroundTask.Unregister();
                    await Cache.InsertObject(ViewModelKeys.ScheduleTileUpdaterActive, false).ToTask();
                }

                return false;
            });

            HandleScheduleBackgroundService.ThrownExceptions.Subscribe(error => UserError.Throw(error.Message, error));

            HandleScheduleBackgroundService.Subscribe(isActive => IsScheduleBackgroundServiceActive = isActive);

            this.ObservableForProperty(p => p.IsScheduleBackgroundServiceActive)
                .Subscribe(isActive => HandleScheduleBackgroundService.Execute(isActive.Value));

            Cache.GetObject<bool>(ViewModelKeys.ScheduleTileUpdaterActive)
                .Catch(Observable.Return(false))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(isActive => IsScheduleBackgroundServiceActive = isActive);

#if WINDOWS_PHONE_APP || WINDOWS_UWP
            IntegrateScheduleToCalendar = ReactivePresenterCommand.CreateAsyncTask(async _ =>
            {
                var schedule = await FetchScheduleImpl();
                await ClientServices().CalendarService.IntegrateScheduleToCalendar(schedule.ToArray());
                return Unit.Default;
            });

            IntegrateScheduleToCalendar.ThrownExceptions.Subscribe(ex =>
            {
                UserError.Throw(ex.Message, ex);
                if (!(ex is ApiException))
                {
                    ViewServices().Popup.ShowMessage(Resources().GetStringForKey("ScheduleIntegrationMessageException"), Resources().GetStringForKey("ScheduleIntegrationTitleException"));
                }
            });

            IntegrateScheduleToCalendar.Messages.Subscribe(x =>
            {
                ViewServices().Popup.ShowMessage(Resources().GetStringForKey("ScheduleIntegrationMessageEmpty"), Resources().GetStringForKey("ScheduleIntegrationTitleEmpty"));
            });

            IntegrateScheduleToCalendar.Subscribe(unit =>
            {
                ViewServices().Popup.ShowMessage(Resources().GetStringForKey("ScheduleIntegrationMessageCompleted"), Resources().GetStringForKey("ScheduleIntegrationTitleCompleted"));
            });

            RemoveScheduleFromCalendar = ReactiveCommand.CreateAsyncTask(async _ =>
            {
                var result = await ClientServices().CalendarService.RemoveScheduleFromCalendar();
                if (!result.Item1)
                {
                    if (result.Item2 == "key_not_found")
                    {
                        ViewServices().Popup.ShowMessage(Resources().GetStringForKey("RemoveScheduleIntegrationMessageNoCalendar"), Resources().GetStringForKey("RemoveScheduleIntegrationTitleNoCalendar"));
                    }
                    return false;
                }
                return true;
            });

            RemoveScheduleFromCalendar.ThrownExceptions.Subscribe(ex =>
            {
                UserError.Throw(ex.Message, ex);
                if (!(ex is ApiException))
                {
                    ViewServices().Popup.ShowMessage(Resources().GetStringForKey("RemoveScheduleIntegrationMessageException"), Resources().GetStringForKey("RemoveScheduleIntegrationTitleException"));
                }
            });

            RemoveScheduleFromCalendar.Subscribe(hasBeenRemoved =>
            {
                if (hasBeenRemoved)
                {
                    ViewServices().Popup.ShowMessage(Resources().GetStringForKey("RemoveScheduleIntegrationMessageCompleted"), Resources().GetStringForKey("RemoveScheduleIntegrationTitleCompleted"));
                }
            });
#endif
        }

        private async Task<Unit> SendLogFilesImpl()
        {
            await LogHelper.ZipApplicationLogsAndSendEmail(Resources().GetStringForKey("SendLogFilesBody"));
            return Unit.Default;
        }

#if WINDOWS_UWP || WINDOWS_PHONE_APP
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
#endif

        #region Properties

        // IAboutViewModel Properties
        //
        public string VersionNumber => $"{Package.Current.Id.Version.Major}.{Package.Current.Id.Version.Minor}.{Package.Current.Id.Version.Build}.{Package.Current.Id.Version.Revision}";
        public Uri SendFeedbackUri { get; private set; }
        public ReactiveCommand<Unit> SendLogFiles { get; private set; }

        // IOptionsViewModel Properties
        //
        public ReactiveCommand<bool> HandleScheduleBackgroundService { get; set; }

        private bool _isScheduleBackgroundServiceActive;

        [DataMember]
        public bool IsScheduleBackgroundServiceActive
        {
            get { return _isScheduleBackgroundServiceActive; }
            set { this.RaiseAndSetIfChanged(ref _isScheduleBackgroundServiceActive, value); }
        }

#if WINDOWS_UWP || WINDOWS_PHONE_APP
        public ReactivePresenterCommand<Unit> IntegrateScheduleToCalendar { get; set; }
        public ReactiveCommand<bool> RemoveScheduleFromCalendar { get; set; }
#endif

        #endregion
    }
}