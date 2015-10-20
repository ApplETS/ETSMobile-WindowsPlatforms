﻿using System;
using System.Reactive.Linq;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Ets.Mobile.ViewModel.Pages.Account;
using ReactiveUI;
using System.Threading.Tasks;

namespace Ets.Mobile.Pages.Account
{
    public sealed partial class LoginPage : Page
    {
        private bool _isLoginShown;

        partial void PartialRegisterBindings()
        {
            var statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            statusBar.BackgroundOpacity = 0;
            statusBar.HideAsync();

            // View ModelGroup
            //RxApp.SuspensionHost.ObserveAppState<LoginViewModel>()
            //    .BindTo(this, x => x.ViewModel);

            // Form
            this.Bind(ViewModel, x => x.UserName, x => x.UserName.Text);
            this.Bind(ViewModel, x => x.Password, x => x.Password.Password);

            // Handle Login Animation State
            this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null)
                .Subscribe(x =>
                {
                    ViewModel.SwitchToLogin = ReactiveCommand.CreateAsyncTask(_ =>
                    {
                        NavigateToVisualState(ShowLogin.Name, true);
                        return Task.FromResult(_isLoginShown);
                    });
                });

            this.BindCommand(ViewModel, x => x.SwitchToLogin, x => x.SwitchToLogin);
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
