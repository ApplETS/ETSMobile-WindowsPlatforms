using Ets.Mobile.ViewModel;
using Ets.Mobile.ViewModel.WinPhone.Pages.ExtendedSplashScreen;
using ReactiveUI;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Ets.Mobile.Pages.ExtendedSplashScreen
{
    public sealed partial class ExtendedSplashScreenPage : Page, IViewFor<ExtendedSplashScreenViewModel>
    {
        #region IViewFor<T>

        public ExtendedSplashScreenViewModel ViewModel
        {
            get { return (ExtendedSplashScreenViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(ExtendedSplashScreenViewModel), typeof(ExtendedSplashScreenPage), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ExtendedSplashScreenViewModel)value; }
        }

        #endregion

        public ExtendedSplashScreenPage()
        {
            InitializeComponent();

            Action<IApplicationShell> navigateWhenReady = appShell => RxApp.TaskpoolScheduler.Schedule(() =>
            {
                appShell.LoadApplicationServices();
                appShell.HandleAuthentificated();
            });

            this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null)
                .Subscribe(vm =>
                {
                    var app = vm.HostScreen as IApplicationShell;
                    if (app == null)
                    {
                        throw new ArgumentException("[ExtendedSplashScreenPage.xaml.cs:43] The Application Shell is null");
                    }
                    navigateWhenReady(app);
                });
        }
    }
}