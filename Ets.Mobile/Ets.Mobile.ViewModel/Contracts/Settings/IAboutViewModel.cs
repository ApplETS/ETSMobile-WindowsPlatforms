using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using ReactiveUI;

namespace Ets.Mobile.ViewModel.Contracts.Settings
{
    public interface IAboutViewModel
    {
        string VersionNumber { get; }
        Uri SendFeedbackUri { get; }
    }
}
