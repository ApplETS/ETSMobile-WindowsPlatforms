using System.Collections.Generic;
using System.Linq;
using Themes.Contracts;
using Themes.Entities;

namespace Ets.Mobile.Skins
{
    public class EtsAppColors : IAppColors
    {
        private const int ColorsCount = 16;

        public static ColorAsString Black { get; } = new ColorAsString("#000000");

        public static ColorAsString LimeGreen { get; } = new ColorAsString("#8ec03f");

        public static ColorAsString LightBlue { get; } = new ColorAsString("#2e7fc2");

        public static ColorAsString Orange { get; } = new ColorAsString("#ea7635");

        public static ColorAsString Red { get; } = new ColorAsString("#c62127");

        public static ColorAsString PinkLight { get; } = new ColorAsString("#eb777d");

        public static ColorAsString Pink { get; } = new ColorAsString("#d44393");

        public static ColorAsString Violet { get; } = new ColorAsString("#a5318d");

        public static ColorAsString BlueGrey { get; } = new ColorAsString("#5e82c1");

        public static ColorAsString Purple { get; } = new ColorAsString("#6f459b");

        public static ColorAsString BlueGreen { get; } = new ColorAsString("#28b893");

        public static ColorAsString GreenDark { get; } = new ColorAsString("#0c8243");

        public static ColorAsString Green { get; } = new ColorAsString("#42b149");

        public static ColorAsString Aqua { get; } = new ColorAsString("#4ab9d2");

        public static ColorAsString OrangeLight { get; } = new ColorAsString("#f2a82f");

        public static ColorAsString Brown { get; } = new ColorAsString("#884b20");

        public static ColorAsString Grey { get; } = new ColorAsString("#919191");

        private static readonly ColorAsString[] Colors =
        {
            LimeGreen,LightBlue,Orange,Red,PinkLight,Pink,Violet,BlueGrey,Purple,BlueGreen,GreenDark,Green,Aqua,OrangeLight,Brown,Grey
        };

        public ColorAsString[] GetColors(int amount)
        {
            if (amount <= 0) return null;

            var colors = new List<ColorAsString>(amount);

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

        public ColorAsString[] GetColorsDescending(int amount)
        {
            if (amount <= 0) return null;

            var colors = new ColorAsString[amount];

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

            return colors.ToArray();
        }
    }
}