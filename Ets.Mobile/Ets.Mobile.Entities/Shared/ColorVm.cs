using System.Drawing;
using System.Runtime.Serialization;

namespace Ets.Mobile.Entities.Shared
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
    }
}