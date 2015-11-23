using System;

namespace Ets.Mobile.ViewModel.Contracts.Settings
{
    public interface IAboutViewModel
    {
        string VersionNumber { get; }
        Uri SendFeedbackUri { get; }
    }
}
