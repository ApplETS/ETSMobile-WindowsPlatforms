using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Ets.Mobile.Content.Shared
{
    public sealed partial class SideNavigation : UserControl
    {
        public SideNavigation()
        {
            InitializeComponent();
        }

        private void DontCheck(object sender, RoutedEventArgs e)
        {
            var s = sender as RadioButton;
            if (s != null)
            {
                s.IsChecked = false;
            }
        }
    }
}