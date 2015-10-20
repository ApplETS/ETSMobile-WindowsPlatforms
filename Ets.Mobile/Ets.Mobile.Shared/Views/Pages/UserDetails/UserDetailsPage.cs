using Windows.UI.Xaml;
using Ets.Mobile.ViewModel.Pages.UserDetails;
using ReactiveUI;

namespace Ets.Mobile.Pages.UserDetails
{
    public partial class UserDetailsPage : IViewFor<UserDetailsViewModel>
    {
        #region IViewFor<T>

        public UserDetailsViewModel ViewModel
        {
            get { return (UserDetailsViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(UserDetailsViewModel), typeof(UserDetailsPage), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (UserDetailsViewModel)value; }
        }

        #endregion

        public UserDetailsPage()
        {
            InitializeComponent();
        }
    }
}
