using ReactiveUI;
using Splat;

namespace Ets.Mobile.ViewModel.UWP.Pages.ExtendedSplashScreen
{
    public class ExtendedSplashScreenViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment { get; } = "ExtendedSplashScreen";
        public IScreen HostScreen { get; }

        public ExtendedSplashScreenViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
        }
    }
}