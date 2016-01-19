//#if WINDOWS_PHONE_APP || WINDOWS_UWP
//using System.Globalization;
//using System.Linq;
//using System.Runtime.InteropServices.WindowsRuntime;
//using Themes.Entities;
//using Windows.UI;

//namespace Themes.Extensions
//{
//    public static class ColorAsStringExtensions
//    {
//        public static Color ToColor(this ColorAsString color)
//        {
//            return new Color
//            {
//                A = byte.Parse(color.HexColor.Substring(7, 2), NumberStyles.AllowHexSpecifier),
//                R = byte.Parse(color.HexColor.Substring(1, 2), NumberStyles.AllowHexSpecifier),
//                G = byte.Parse(color.HexColor.Substring(3, 2), NumberStyles.AllowHexSpecifier),
//                B = byte.Parse(color.HexColor.Substring(5, 2), NumberStyles.AllowHexSpecifier)
//            };
//        }

//        public static Color[] ToColorArray([ReadOnlyArray] this ColorAsString[] colors)
//        {
//            return colors.Select(c => c.ToColor()).ToArray();
//        }
//    }
//}
//#endif