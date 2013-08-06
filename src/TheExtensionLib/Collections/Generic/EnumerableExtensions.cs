using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> e, Action<T> action)
        {
            foreach (T item in e)
                action(item);
        }

        public static void ForEachWithIndex<T>(this IEnumerable<T> e, Action<T, int> action)
        {
            int idx = 0;
            foreach (var item in e)
                action(item, idx++);
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> e)
        {
            return e == null || !e.Any();
        }

        public static string ToString<T>(this IEnumerable<T> e, string separator)
        {
            return string.Join(separator, e);
        }

        /// <summary>
        /// ref: http://stackoverflow.com/a/5304033/937411
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="parts"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int parts)
        {
            return list.Select((item, index) => new { index, item })
                       .GroupBy(x => x.index % parts)
                       .Select(x => x.Select(y => y.item));
        }

        /// <summary>
        /// ref: http://stackoverflow.com/a/3382769/937411
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Section<T>(this IEnumerable<T> source, int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException("length");

            var section = new List<T>(length);

            foreach (var item in source)
            {
                section.Add(item);

                if (section.Count == length)
                {
                    yield return section.AsReadOnly();
                    section = new List<T>(length);
                }
            }

            if (section.Count > 0)
                yield return section.AsReadOnly();
        }
    }
}
