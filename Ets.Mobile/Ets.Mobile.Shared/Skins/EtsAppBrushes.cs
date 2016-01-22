using Themes.Contracts;
using Themes.Entities;

namespace Themes
{
    /// <summary>
    /// ÉTSMobile Default Colors
    /// <para>Ref: <see cref="https://color.adobe.com/AppETS-color-theme-3400460/">Here</see></para>
    /// </summary>
    public sealed class EtsAppBrushes : IAppBrush
    {
        public ColorAsString HighestBrush { get; } = new ColorAsString("#680100");
        
        public ColorAsString HighBrush { get; } = new ColorAsString("#7F0100");
        
        public ColorAsString MediumBrush { get; } = new ColorAsString("#CC0100");
        
        public ColorAsString MidBrush { get; } = new ColorAsString("#FF0200");
        
        public ColorAsString LowestBrush { get; } = new ColorAsString("#FF4E4C");
    }
}