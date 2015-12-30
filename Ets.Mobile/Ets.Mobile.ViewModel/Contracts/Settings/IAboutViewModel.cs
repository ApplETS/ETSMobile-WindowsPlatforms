using System;
using System.Reactive;
using ReactiveUI;

namespace Ets.Mobile.ViewModel.Contracts.Settings
{
    public interface IAboutViewModel
    {
        string VersionNumber { get; }
        Uri SendFeedbackUri { get; }
        ReactiveCommand<Unit> SendLogFiles { get; }
    }
}