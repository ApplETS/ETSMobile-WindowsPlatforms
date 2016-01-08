using Ets.Mobile.ViewModel.Pages.Account;
using ReactiveUI;
using System;
using System.Reactive.Linq;
using Windows.UI.Xaml;

namespace Ets.Mobile.Pages.Account
{
    public partial class LoginPage : IViewFor<LoginViewModel>
    {
        #region IViewFor<T>

        public LoginViewModel ViewModel
        {
            get { return (LoginViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(LoginViewModel), typeof(LoginPage), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (LoginViewModel)value; }
        }

        #endregion

        public LoginPage()
        {
            InitializeComponent();

            // When ViewModel is set
            this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null)
                .Subscribe(x =>
                {
#if DEBUG
                    ViewModel.UserName = "";
                    ViewModel.Password = "";
#endif
                    SubmitCommand.Click += (sender, arg) => { ErrorMessage.Visibility = Visibility.Collapsed; };
                });

            // Error Handling
            UserError.RegisterHandler(ue =>
            {
                ErrorMessage.Text = ue.ErrorMessage;
                ErrorMessage.Visibility = Visibility.Visible;

                return Observable.Return(RecoveryOptionResult.CancelOperation);
            });

            this.BindCommand(ViewModel, x => x.SubmitCommand, x => x.SubmitCommand);

            PartialInitialize();
        }

        partial void PartialInitialize();
    }
}