using System.Collections.Generic;
using System.Linq;

namespace Ets.Mobile.ViewModel.Comparators
{
    public static class SemestersComparatorMethod
    {
        public static int Compare(string obj1, string obj2)
        {
            if(int.Parse(obj1.Substring(1, 4)) != int.Parse(obj2.Substring(1, 4)))
            {
                return int.Parse(obj1.Substring(1, 4)).CompareTo(int.Parse(obj2.Substring(1, 4)));
            }

            switch (obj1.First())
            {
                case 'A':
                    if (obj2.First() == 'H' || obj2.First() == 'É' || obj2.First() == 'E')
                    {
                        return 1;
                    }
                    break;
                case 'H':
                    if (obj2.First() == 'A' || obj2.First() == 'É' || obj2.First() == 'E')
                    {
                        return -1;
                    }
                    break;
                case 'É':
                case 'E':
                    if (obj2.First() == 'A')
                    {
                        return -1;
                    }
                    else if (obj2.First() == 'H')
                    {
                        return 1;
                    }
                    break;
            }

            return 0;
        }

        public static int ReversedCompare(string obj1, string obj2)
        {
            if (int.Parse(obj1.Substring(1, 4)) != int.Parse(obj2.Substring(1, 4)))
            {
                return -int.Parse(obj1.Substring(1, 4)).CompareTo(int.Parse(obj2.Substring(1, 4)));
            }

            switch (obj1.First())
            {
                case 'A':
                    if (obj2.First() == 'H' || obj2.First() == 'É' || obj2.First() == 'E')
                    {
                        return -1;
                    }
                    break;
                case 'H':
                    if (obj2.First() == 'A' || obj2.First() == 'É' || obj2.First() == 'E')
                    {
                        return 1;
                    }
                    break;
                case 'É':
                case 'E':
                    if (obj2.First() == 'A')
                    {
                        return 1;
                    }
                    else if (obj2.First() == 'H')
                    {
                        return -1;
                    }
                    break;
            }

            return 0;
        }
    }

    public class SemestersComparator : IComparer<string>
    {
        public int Compare(string obj1, string obj2)
        {
            if (int.Parse(obj1.Substring(1, 4)) != int.Parse(obj2.Substring(1, 4)))
            {
                return int.Parse(obj1.Substring(1, 4)).CompareTo(int.Parse(obj2.Substring(1, 4)));
            }

            switch (obj1.First())
            {
                case 'A':
                    if (obj2.First() == 'H' || obj2.First() == 'É' || obj2.First() == 'E')
                    {
                        return 1;
                    }
                    break;
                case 'H':
                    if (obj2.First() == 'A' || obj2.First() == 'É' || obj2.First() == 'E')
                    {
                        return -1;
                    }
                    break;
                case 'É':
                case 'E':
                    if (obj2.First() == 'A')
                    {
                        return -1;
                    }
                    else if (obj2.First() == 'H')
                    {
                        return 1;
                    }
                    break;
            }

            return 0;
        }
    }
}
