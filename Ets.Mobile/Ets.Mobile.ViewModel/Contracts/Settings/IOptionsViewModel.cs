using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;
using System.Reactive;

namespace Ets.Mobile.ViewModel.Contracts.Settings
{
    public interface IOptionsViewModel
    {
        /// <summary>
        /// We want to toggle weither we want to see the schedule or to
        /// remove a background service
        /// </summary>
        ReactiveCommand<bool> HandleScheduleBackgroundService { get; set; }
        bool IsScheduleBackgroundServiceActive { get; set; }
#if WINDOWS_PHONE_APP || WINDOWS_UWP
        /// <summary>
        /// Integrate the schedule to the device's calendar
        /// </summary>
        ReactivePresenterCommand<Unit> IntegrateScheduleToCalendar { get; set; }
        /// <summary>
        /// Remove the schedule from the device's calendar
        /// </summary>
        ReactiveCommand<bool> RemoveScheduleFromCalendar { get; set; }
#endif
    }
}