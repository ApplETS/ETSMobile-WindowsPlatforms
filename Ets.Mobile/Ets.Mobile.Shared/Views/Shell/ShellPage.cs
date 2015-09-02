using Windows.UI.Xaml.Controls;
using ReactiveUI;
using Splat;

namespace Ets.Mobile.Shell
{
    public partial class ShellPage : Page
    {
        public ShellPage()
        {
            InitializeComponent();
            DataContext = Locator.Current.GetService(typeof(IScreen));
            PartialInitialize();
        }

        partial void PartialInitialize();
    }
}
