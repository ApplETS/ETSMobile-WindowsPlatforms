using Windows.UI;

namespace Ets.Mobile.Entities.Signets.Interfaces
{
    public interface ICustomColor
    {
        string Color { get; set; }
        void SetNewColor(ColorVm color);
    }
}
