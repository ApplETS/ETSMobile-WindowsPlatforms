using Ets.Mobile.ViewModel.Pages.Account;
using ReactiveUI;
using System;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Ets.Mobile.Pages.Account
{
    public partial class LoginPage : IViewFor<LoginPageViewModel>
    {
        #region IViewFor<T>

        public LoginPageViewModel ViewModel
        {
            get { return (LoginPageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(LoginPageViewModel), typeof(LoginPage), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (LoginPageViewModel)value; }
        }

        #endregion

        public LoginPage()
        {
            InitializeComponent();

            Login.Events().Click.Subscribe(arg =>
            {
                ErrorMessage.Visibility = Visibility.Collapsed;
            });

            // Error Handling
            UserError.RegisterHandler(ue =>
            {
                ErrorMessage.Text = ue.ErrorMessage;
                ErrorMessage.Visibility = Visibility.Visible;

                return Observable.Return(RecoveryOptionResult.CancelOperation);
            });
            
            PartialInitialize();
        }

        partial void PartialInitialize();
    }
}