using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Ets.Mobile.Entities.Signets.Interfaces
{
    public interface ICustomColor
    {
        byte A { get; set; }
        byte R { get; set; }
        byte G { get; set; }
        byte B { get; set; }
        Color Color { get; set; }
        SolidColorBrush Brush { get; }
    }
}
