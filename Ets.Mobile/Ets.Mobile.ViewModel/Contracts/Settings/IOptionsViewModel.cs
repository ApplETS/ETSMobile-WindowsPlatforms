using ReactiveUI;

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
    }
}