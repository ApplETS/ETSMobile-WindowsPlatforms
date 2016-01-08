using ReactiveUI;
using System;
using System.Reactive;

namespace Ets.Mobile.ViewModel.Contracts.Settings
{
    public interface IAboutViewModel
    {
        string VersionNumber { get; }
        Uri SendFeedbackUri { get; }
        ReactiveCommand<Unit> SendLogFiles { get; }
    }
}