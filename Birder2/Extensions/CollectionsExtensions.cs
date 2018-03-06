using System.Collections.Generic;
using System.Linq;

namespace Birder2.Extensions
{
    public static class CollectionsExtensions
    {
        public static IEnumerable<T> Append<T>(this IEnumerable<T> rest, params T[] last) => rest.Concat(last);
    }
}
