using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Universal.UI.Xaml.Controls
{
    public class SplitViewGrid : Grid
    {
        public SplitViewGrid()
        {
            RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(60)
                });
            RowDefinitions.Add(new RowDefinition
                {
                    Height = GridLength.Auto
                });
        }
    }
}
