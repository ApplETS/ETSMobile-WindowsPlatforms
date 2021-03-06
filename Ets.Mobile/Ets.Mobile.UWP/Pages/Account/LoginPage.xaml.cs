﻿using ReactiveUI;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Ets.Mobile.Pages.Account
{
    public sealed partial class LoginPage : Page
    {
        private bool _isLoginShown;

        partial void PartialInitialize()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
                statusBar.BackgroundOpacity = 0;
                statusBar.HideAsync().GetResults();
            }

            // View ModelGroup
            //RxApp.SuspensionHost.ObserveAppState<LoginPageViewModel>()
            //    .BindTo(this, x => x.ViewModel);
            
            var subscriptionForViewModel = this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null);

            subscriptionForViewModel.BindTo(this, x => x.DataContext);

            subscriptionForViewModel
                .Subscribe(vm =>
                {
                    vm.SwitchToLogin = ReactiveCommand.CreateAsyncTask(_ =>
                    {
                        NavigateToVisualState(ShowLogin.Name, true);
                        return Task.FromResult(_isLoginShown);
                    });
                });
        }

        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (_isLoginShown)
            {
                NavigateToVisualState(HideLogin.Name, false);
                e.Handled = true;
            }
        }

        private void NavigateToVisualState(string key, bool navigateToLogin)
        {
            VisualStateManager.GoToState(this, key, true);
            _isLoginShown = navigateToLogin;
            if (_isLoginShown)
            {
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            }
            else
            {
                HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            }
        }

        private void OnLoginShown(object sender, object e)
        {
            UserName.Focus(FocusState.Keyboard);
        }
    }
}