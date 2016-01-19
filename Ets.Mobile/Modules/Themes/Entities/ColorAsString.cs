namespace Themes.Entities
{
    public sealed class ColorAsString
    {
        public ColorAsString(string hex)
        {
            HexColor = hex;
        }

        public string HexColor { get; }
    }
}