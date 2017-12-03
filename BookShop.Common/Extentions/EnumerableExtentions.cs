using System.Collections.Generic;

namespace BookShop.Common.Extentions
{
    public static class EnumerableExtentions
    {
        public static ISet<T> ToHashSet<T>(this IEnumerable<T> enumerable)
        {
            return new HashSet<T>(enumerable);
        }
    }
}
