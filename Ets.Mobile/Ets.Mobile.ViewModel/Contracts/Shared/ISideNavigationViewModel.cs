using System;
using System.Reactive;
using System.Reactive.Subjects;
using ReactiveUI;
using Ets.Mobile.ViewModel.Contracts.UserDetails;

namespace Ets.Mobile.ViewModel.Contracts.Shared
{
    public interface ISideNavigationViewModel
    {
        ReactiveCommand<Unit> Logout { get; set; }
        IUserDetailsViewModel UserDetails { get; set; }
        string CurrentPage { get; set; }
        Type CurrentViewModelType { get; set; }
        bool IsSideNavigationVisible { get; set; }
        ISubject<bool> IsSideNavigationVisibleSubject { get; set; }
        ReactiveCommand<bool> OpenMenu { get; set; }
        ReactiveCommand<bool> CloseMenu { get; set; }
    }
}