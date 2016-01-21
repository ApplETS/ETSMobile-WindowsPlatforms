using Ets.Mobile.Entities.Auth;
using ReactiveUI;

namespace Ets.Mobile.ViewModel.Contracts.Account
{
    public interface ILoginPageViewModel
    {
        string UserName { get; set; }
        string Password { get; set; }
        ReactiveCommand<bool> SwitchToLogin { get; set; }
        ReactiveCommand<EtsUserCredentials> Login { get; set; }
    }
}