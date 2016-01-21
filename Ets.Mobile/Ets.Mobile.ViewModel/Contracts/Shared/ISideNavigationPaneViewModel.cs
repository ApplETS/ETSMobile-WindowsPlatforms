using Ets.Mobile.ViewModel.Contracts.UserDetails;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Subjects;

namespace Ets.Mobile.ViewModel.Contracts.Shared
{
    public interface ISideNavigationPaneViewModel
    {
        ReactiveCommand<Unit> Logout { get; set; }
        IUserDetailsPageViewModel UserDetails { get; set; }
        string CurrentPage { get; set; }
        Type CurrentViewModelType { get; set; }
        bool IsSideNavigationVisible { get; set; }
        ISubject<bool> IsSideNavigationVisibleSubject { get; set; }
        ReactiveCommand<bool> OpenMenu { get; set; }
        ReactiveCommand<bool> CloseMenu { get; set; }
    }
}