using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Contracts.Settings;
using Ets.Mobile.ViewModel.Helpers;
using ReactiveUI;
using System;
using System.Reactive;
using System.Threading.Tasks;
using Windows.ApplicationModel;
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
        }

        private async Task<Unit> SendLogFilesImpl()
        {
            await LogHelper.ZipApplicationLogsAndSendEmail(Resources().GetString("SendLogFilesBody"));
            return Unit.Default;
        }

        #region Properties
        
        public string VersionNumber => $"{Package.Current.Id.Version.Major}.{Package.Current.Id.Version.Minor}.{Package.Current.Id.Version.Build}.{Package.Current.Id.Version.Revision}";
        public Uri SendFeedbackUri { get; private set; }
        public ReactiveCommand<Unit> SendLogFiles { get; private set; }
        
        #endregion
    }
}