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

        /// <summary>
        /// Replaces first element that satisfied the predicate with replacement, returns new sequence.
        /// </summary>
        public static IEnumerable<T> Replace<T>(this IEnumerable<T> source, Func<T, bool> which, T replacement)
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

        /// <summary>
        /// Removes first element that satisfied the predicate, returns new sequence.
        /// </summary>
        public static IEnumerable<T> Remove<T>(this IEnumerable<T> source, Func<T, bool> which)
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

        public static IEnumerable<T> RemoveAll<T>(this IEnumerable<T> source, Func<T, bool> which)
        {
            foreach (var item in source)
            {
                if (which(item)) { }
                else
                    yield return item;
            }
        }

        public static IEnumerable<T> Tap<T>(this IEnumerable<T> source, Action<IEnumerable<T>> action)
        {
            action(source);
            return source;
        }
    }
}
