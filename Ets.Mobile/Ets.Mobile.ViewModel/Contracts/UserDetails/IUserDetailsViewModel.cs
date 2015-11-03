﻿using System;
using System.Collections.Generic;
using System.Text;
using Ets.Mobile.Entities.Signets;
using ReactiveUI;

namespace Ets.Mobile.ViewModel.Contracts.UserDetails
{
    public interface IUserDetailsViewModel
    {
        UserDetailsVm Profile { get; set; }
        ReactiveCommand<UserDetailsVm> LoadProfile { get; } 
    }
}
