using System;
using Windows.ApplicationModel;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Contracts.Settings;
using ReactiveUI;

namespace Ets.Mobile.ViewModel.Pages.Settings
{
    public class SettingsViewModel : ViewModelBase, ISettingsViewModel
    {
        public string VersionNumber => $"{Package.Current.Id.Version.Major}.{Package.Current.Id.Version.Minor}.{Package.Current.Id.Version.Revision}.{Package.Current.Id.Version.Build}";
        public Uri SendFeedbackUri { get; private set; }

        public SettingsViewModel(IScreen screen) : base(screen, "Settings")
        {
            OnViewModelCreation();
        }

        protected sealed override void OnViewModelCreation()
        {
            SendFeedbackUri = new Uri("mailto:clubapplets@googlegroups.com?subject=" +
#if WINDOWS_PHONE_APP
            "ÉtsMobile-WindowsPhone"
#else
            "ÉtsMobile-Windows"
#endif
            );
        }
    }
}