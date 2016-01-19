namespace Ets.Mobile.Entities.Shared
{
    public interface ICustomColor
    {
        string Color { get; set; }
        void SetNewColor(ColorVm color);
    }
}