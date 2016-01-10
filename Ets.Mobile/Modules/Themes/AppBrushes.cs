using Windows.UI;

namespace Themes
{
    /// <summary>
    /// Ets Mobile Default Colors
    /// <para>Ref: <see cref="https://color.adobe.com/AppETS-color-theme-3400460/">Here</see></para>
    /// </summary>
    public sealed class AppBrushes
    {
        /// <summary>
        /// #680100
        /// </summary>
        public static Color Background { get; } = new Color
        {
            A = 255,
            R = 255,
            G = 255,
            B = 255
        };

        /// <summary>
        /// #680100
        /// </summary>
        public static Color MainBackground { get; } = new Color
        {
            A = 255,
            R = 241,
            G = 242,
            B = 243
        };


        /// <summary>
        /// #680100
        /// </summary>
        public static Color HighestBrush { get; } = new Color
        {
            A = 255,
            R = 104,
            G = 1,
            B = 0
        };

        /// <summary>
        /// #7F0100
        /// </summary>
        public static Color HighBrush { get; } = new Color
        {
            A = 255,
            R = 127,
            G = 1,
            B = 0
        };

        /// <summary>
        /// #CC0100
        /// </summary>
        public static Color MediumBrush { get; } = new Color
        {
            A = 255,
            R = 204,
            G = 1,
            B = 0
        };

        /// <summary>
        /// #FF0200
        /// </summary>
        public static Color MidBrush { get; } = new Color
        {
            A = 255,
            R = 255,
            G = 2,
            B = 0
        };

        /// <summary>
        /// #FF4E4C
        /// </summary>
        public static Color LowestBrush { get; } = new Color
        {
            A = 255,
            R = 255,
            G = 78,
            B = 76
        };
    }
}