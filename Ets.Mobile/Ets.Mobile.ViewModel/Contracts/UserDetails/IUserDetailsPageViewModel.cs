using Ets.Mobile.Entities.Signets;
using ReactiveUI;

namespace Ets.Mobile.ViewModel.Contracts.UserDetails
{
    public interface IUserDetailsPageViewModel
    {
        UserDetailsVm Profile { get; set; }
        ReactiveCommand<UserDetailsVm> LoadProfile { get; } 
    }
}