using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Ets.Mobile.Entities.Signets.Interfaces
{
    [DataContract]
    public class ColorVm
    {
        public ColorVm(Color color)
        {
            A = color.A;
            R = color.R;
            G = color.G;
            B = color.B;
        }

        [DataMember]
        public byte A { get; set; }
        [DataMember]
        public byte R { get; set; }
        [DataMember]
        public byte G { get; set; }
        [DataMember]
        public byte B { get; set; }
    }
}
