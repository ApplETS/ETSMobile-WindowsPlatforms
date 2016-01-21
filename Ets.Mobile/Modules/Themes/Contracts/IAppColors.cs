using Themes.Entities;

namespace Themes.Contracts
{
    public interface IAppColors
    {
        ColorAsString[] GetColors(int amount);
        ColorAsString[] GetColorsDescending(int amount);
    }
}