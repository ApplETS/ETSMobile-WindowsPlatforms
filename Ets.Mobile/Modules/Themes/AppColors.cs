using System.Collections.Generic;
using Windows.UI;

namespace Themes
{
    public static class AppColors
    {
        private const int ColorsCount = 16;

        public static Color Black { get; } = new Color
        {
            A = 0,
            R = 0,
            G = 0,
            B = 0
        };

        public static Color LimeGreen { get; } = new Color
        {
            A = 255,
            R = 142,
            G = 192,
            B = 63
        };

        public static Color LightBlue { get; } = new Color
        {
            A = 255,
            R = 46,
            G = 127,
            B = 194
        };

        public static Color Orange { get; } = new Color
        {
            A = 255,
            R = 234,
            G = 118,
            B = 53
        };

        public static Color Red { get; } = new Color
        {
            A = 255,
            R = 198,
            G = 33,
            B = 39
        };

        public static Color PinkLight { get; } = new Color
        {
            A = 255,
            R = 235,
            G = 119,
            B = 125
        };

        public static Color Pink { get; } = new Color
        {
            A = 255,
            R = 212,
            G = 67,
            B = 147
        };

        public static Color Violet { get; } = new Color
        {
            A = 255,
            R = 165,
            G = 49,
            B = 141
        };

        public static Color BlueGrey { get; } = new Color
        {
            A = 255,
            R = 94,
            G = 130,
            B = 193
        };

        public static Color Purple { get; } = new Color
        {
            A = 255,
            R = 111,
            G = 69,
            B = 155
        };

        public static Color BlueGreen { get; } = new Color
        {
            A = 255,
            R = 40,
            G = 184,
            B = 147
        };

        public static Color GreenDark { get; } = new Color
        {
            A = 255,
            R = 12,
            G = 130,
            B = 67
        };

        public static Color Green { get; } = new Color
        {
            A = 255,
            R = 66,
            G = 177,
            B = 73
        };

        public static Color Aqua { get; } = new Color
        {
            A = 255,
            R = 74,
            G = 185,
            B = 210
        };

        public static Color OrangeLight { get; } = new Color
        {
            A = 255,
            R = 242,
            G = 168,
            B = 47
        };

        public static Color Brown { get; } = new Color
        {
            A = 255,
            R = 136,
            G = 75,
            B = 32
        };

        public static Color Grey { get; } = new Color
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

        public static Color[] GetColors(int amount)
        {
            if (amount <= 0) return null;

            var colors = new List<Color>(amount);

            var k = 0;

            for (var i = 0; i < amount; i++)
            {
                if (k % ColorsCount == 0)
                {
                    k = 0;
                }
                colors.Add(Colors[k++]);
            }

            return colors.ToArray();
        }

        public static Color[] GetColorsDescending(int amount)
        {
            if (amount <= 0) return null;

            var colors = new Color[amount];

            var k = 0;

            for (var i = 0; i < colors.Length; i++)
            {
                if (k % ColorsCount != 0)
                {
                    colors[i] = Colors[k++];
                }
                else
                {
                    k = 0;
                    colors[i] = Colors[k++];
                }
            }

            return colors;
        }
    }
}