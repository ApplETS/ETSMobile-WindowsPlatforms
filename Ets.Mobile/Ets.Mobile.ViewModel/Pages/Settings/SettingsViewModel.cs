using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Contracts.Settings;
using Ets.Mobile.ViewModel.Helpers;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Akavache;
#if WINDOWS_PHONE_APP || WINDOWS_UWP
using Windows.ApplicationModel.Email;
#endif

namespace Ets.Mobile.ViewModel.Pages.Settings
{
    public class SettingsViewModel : ViewModelBase, ISettingsViewModel
    {
        public SettingsViewModel(IScreen screen) : base(screen, "Settings")
        {
            OnViewModelCreation();
        }

        protected sealed override void OnViewModelCreation()
        {
            IsScheduleBackgroundServiceActive = true;
            this.ObservableForProperty(p => p.IsScheduleBackgroundServiceActive)
                .Subscribe(isActive => HandleScheduleBackgroundService.Execute(isActive.Value));

            Cache.GetObject<bool>(ViewModelKeys.ScheduleTileUpdaterActive)
                .Subscribe(isActive => IsScheduleBackgroundServiceActive = isActive);

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
                await Task.Delay(2000);
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
        }

        private async Task<Unit> SendLogFilesImpl()
        {
            await LogHelper.ZipApplicationLogsAndSendEmail(Resources().GetString("SendLogFilesBody"));
            return Unit.Default;
        }

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

        #endregion
    }
}