using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using Ets.Mobile.ViewModel.Pages.UserDetails;
using ReactiveUI;
using Ets.Mobile.ViewModel.Contracts.UserDetails;

namespace Ets.Mobile.ViewModel.Contracts.Shared
{
    public interface ISideNavigationViewModel
    {
        ReactiveCommand<Unit> Logout { get; set; }

        ReactiveCommand<Unit> NavigateToUserDetails { get; set; }

        IUserDetailsViewModel UserDetails { get; set; }
        string CurrentPage { get; set; }
    }
}
