using Themes.Entities;

namespace Themes.Contracts
{
    public interface IAppBrush
    {
        ColorAsString HighestBrush { get; }
        ColorAsString HighBrush { get; }
        ColorAsString MediumBrush { get; }
        ColorAsString MidBrush { get; }
        ColorAsString LowestBrush { get; }
    }
}