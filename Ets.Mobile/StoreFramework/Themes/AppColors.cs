using Windows.UI;

namespace StoreFramework.Themes
{
    public static class AppColors
    {
        private const int _ColorsCount = 16;

        /// <summary>
        /// Black - Default Color
        /// </summary>
        public static readonly Color Black = new Color
        {
            A = 0,
            R = 0,
            G = 0,
            B = 0
        };

        public static readonly Color LimeGreen = new Color
        {
            A = 255,
            R = 142,
            G = 192,
            B = 63
        };

        public static readonly Color LightBlue = new Color
        {
            A = 255,
            R = 46,
            G = 127,
            B = 194
        };

        public static readonly Color Orange = new Color
        {
            A = 255,
            R = 234,
            G = 118,
            B = 53
        };

        public static readonly Color Red = new Color
        {
            A = 255,
            R = 198,
            G = 33,
            B = 39
        };

        public static readonly Color PinkLight = new Color
        {
            A = 255,
            R = 235,
            G = 119,
            B = 125
        };

        public static readonly Color Pink = new Color
        {
            A = 255,
            R = 212,
            G = 67,
            B = 147
        };

        public static readonly Color Violet = new Color
        {
            A = 255,
            R = 165,
            G = 49,
            B = 141
        };

        public static readonly Color BlueGrey = new Color
        {
            A = 255,
            R = 94,
            G = 130,
            B = 193
        };

        public static readonly Color Purple = new Color
        {
            A = 255,
            R = 111,
            G = 69,
            B = 155
        };

        public static readonly Color BlueGreen = new Color
        {
            A = 255,
            R = 40,
            G = 184,
            B = 147
        };

        public static readonly Color GreenDark = new Color
        {
            A = 255,
            R = 12,
            G = 130,
            B = 67
        };

        public static readonly Color Green = new Color
        {
            A = 255,
            R = 66,
            G = 177,
            B = 73
        };

        public static readonly Color Aqua = new Color
        {
            A = 255,
            R = 74,
            G = 185,
            B = 210
        };

        public static readonly Color OrangeLight = new Color
        {
            A = 255,
            R = 242,
            G = 168,
            B = 47
        };

        public static readonly Color Brown = new Color
        {
            A = 255,
            R = 136,
            G = 75,
            B = 32
        };

        public static readonly Color Grey = new Color
        {
            A = 145,
            R = 145,
            G = 145,
            B = 145
        };

        private static readonly Color[] Colors =
        {
            LimeGreen,LightBlue,Orange,Red,PinkLight,Pink,Violet,BlueGrey,Purple,BlueGreen,GreenDark,Green,Aqua,OrangeLight,Brown,Grey
        };

        public static Color[] GetColors(int amount, bool descending = false)
        {
            if (amount <= 0) return null;

            var colors = new Color[amount];

            var k = (descending) ? _ColorsCount-1 : 0;

            if (!descending)
            {
                for (var i = 0; i < colors.Length; i++)
                {
                    if (k%_ColorsCount != 0)
                        colors[i] = Colors[k++];
                    else
                    {
                        k = 0;
                        colors[i] = Colors[k++];
                    }
                }
            }
            else
            {
                for (var i = 0; i < amount; i++)
                {
                   if (k % _ColorsCount != 0)
                        colors[i] = Colors[k--];
                    else
                    {
                        k = _ColorsCount;
                        colors[i] = Colors[k--];
                    } 
                }
            }

            return colors;
        }
    }
}
