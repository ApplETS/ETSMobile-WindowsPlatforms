using System.Globalization;
using System.Runtime.Serialization;
using Windows.UI;

namespace Ets.Mobile.Entities.Signets.Interfaces
{
    [DataContract]
    public class ColorVm
    {
        public ColorVm(Color color)
        {
            HexColor = color.ToString();
        }

        public ColorVm(string hexCode)
        {
            if (string.IsNullOrEmpty(hexCode))
            {
                hexCode = "#FFFFFFFF";
            }
            HexColor = hexCode;
        }

        public ColorVm()
        {
        }

        [DataMember]
        public string HexColor { get; set; }

        public Color ToColor()
        {
            var color = new Color
            {
                A = byte.Parse(HexColor.Substring(7, 2), NumberStyles.AllowHexSpecifier),
                R = byte.Parse(HexColor.Substring(1, 2), NumberStyles.AllowHexSpecifier),
                G = byte.Parse(HexColor.Substring(3, 2), NumberStyles.AllowHexSpecifier),
                B = byte.Parse(HexColor.Substring(5, 2), NumberStyles.AllowHexSpecifier)
            };
            return color;
        }
    }
}
