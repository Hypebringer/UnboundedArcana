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
        public static IEnumerable<T> ConcatSingle<T>(this IEnumerable<T> source, T added)
        {
            foreach (T item in source)
                yield return item;
            yield return added; 
        }
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

        public static IEnumerable<T> ReplaceFirst<T>(this IEnumerable<T> source, Func<T, bool> which, T replacement)
        {
            bool replaced = false;
            foreach (var item in source)
            {
                if (!replaced && which(item))
                {
                    yield return replacement;
                    replaced = true;
                }
                else
                    yield return item;
            }
        }

        public static IEnumerable<T> RemoveFirst<T>(this IEnumerable<T> source, Func<T, bool> which)
        {
            bool removed = false;
            foreach (var item in source)
            {
                if (!removed && which(item))
                    removed = true;
                else
                    yield return item;
            }
        }
    }
}
