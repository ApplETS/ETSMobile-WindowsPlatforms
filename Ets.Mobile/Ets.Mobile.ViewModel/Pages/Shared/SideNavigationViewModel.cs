using System.Reactive;
using System.Reactive.Threading.Tasks;
using Ets.Mobile.Business.Entities.Results.Signets.Interfaces;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Contracts;
using Ets.Mobile.ViewModel.Pages.Account;
using Ets.Mobile.ViewModel.Pages.UserDetails;
using ReactiveUI;
using ReactiveUI.Extensions;
using Splat;
using Ets.Mobile.ViewModel.Contracts.Shared;
using Ets.Mobile.ViewModel.Contracts.UserDetails;

namespace Ets.Mobile.ViewModel.Pages.Shared
{
    public class SideNavigationViewModel : ApplicationViewModelBase, ISideNavigationViewModel
    {
        public SideNavigationViewModel(IScreen screen, IUserDetailsViewModel userDetails)
        {
            Screen = screen ?? Locator.Current.GetService<IScreen>();
            UserDetails = userDetails;
            OnViewModelCreation();
        }

        protected override sealed void OnViewModelCreation()
        {
            Logout = ReactiveCommand.CreateAsyncTask(async _ =>
            {
                await Cache.InvalidateAll().ToTask();
                Screen.Router.NavigateAndReset.Execute(new LoginViewModel(Locator.Current.GetService<IScreen>()));
                return Unit.Default;
            });

            NavigateToUserDetails = Screen.Router.CreateNavigationAsyncCommand(new UserDetailsViewModel(Locator.Current.GetService<IScreen>()));
        }

        #region Properties

        private IScreen Screen { get; set; }

        public ReactiveCommand<Unit> Logout { get; set; }

        public ReactiveCommand<Unit> NavigateToUserDetails { get; set; }

        private IUserDetailsViewModel _userDetails;
        public IUserDetailsViewModel UserDetails
        {
            get { return _userDetails; }
            set { this.RaiseAndSetIfChanged(ref _userDetails, value); }
        }

        private string _currentPage;
        public string CurrentPage
        {
            get { return _currentPage; }
            set { this.RaiseAndSetIfChanged(ref _currentPage, value); }
        }

        #endregion
    }
}