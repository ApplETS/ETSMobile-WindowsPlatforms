using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Ets.Mobile.Views.Content.Shared
{
    public sealed partial class SideNavigation : UserControl
    {
        public SideNavigation()
        {
            InitializeComponent();
        }

        //public object ProfileDataContext
        //{
        //    get { return Profile.DataContext; }
        //    set { Profile.DataContext = value; }
        //}

        //public ICommand RefreshProfileCommand
        //{
        //    get { return RefreshProfile.Command; }
        //    set { RefreshProfile.Command = value; }
        //}

        //public ICommand ProfileTappedCommand { get; set; }

        //public ICommand LogoutCommand
        //{
        //    get { return Logout.Command; }
        //    set { Logout.Command = value; }
        //}

        //private void ProfileTapped(object sender, TappedRoutedEventArgs e)
        //{
        //    ProfileTappedCommand?.Execute(null);
        //}
    }
}
