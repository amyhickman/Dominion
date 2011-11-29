using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.Util
{
    public static class Extensions
    {
        public static IList<T> Shuffle<T>(this IList<T> items)
        {
            for (int i = items.Count - 1; i > 0; i--)
            {
                int k = RNG.Next(0, i);
                T tmp = items[k];
                items[k] = items[i];
                items[i] = tmp;
            }
            return items;
        }
    }
}
