using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnboundedArcana.Extensions
{
    static class MoreEnumerable
    {
        public static IEnumerable<T> Singleton<T>(this T source) { yield return source; }
        public static T FirstOfType<T>(this IEnumerable source)
        {
            foreach (var item in source)
            {
                if (item is T first)
                    return first;
            }
            throw new ArgumentException($"No value of type {nameof(T)} found");
        }

        public static T FirstOrDefaultOfType<T>(this IEnumerable source)
        {
            foreach (var item in source)
            {
                if (item is T first)
                    return first;
            }
            return default(T);
        }
    }
}
