using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Collections.Generic
{
    public static class ListExtensions
    {
        public static T GetItemOrDefault<T>(this IList<T> list, int index) where T : class
        {
            if (index < 0 || index > list.Count - 1)
                return default(T);
            return list[index];
        }
        public static T GetItemOrDefault<T>(this IList<T> list, int index, T @default) where T : class
        {
            if (index < 0 || index > list.Count - 1)
                return @default;
            return list[index];
        }

        public static void AddIfNotNull<T>(this IList<T> list, T item) where T : class
        {
            if (item == null)
                return;
            list.Add(item);
        }
        public static void AddIfNotNull<T>(this IList<T> list, IEnumerable<T> items) where T : class
        {
            if (items == null)
                return;
            items.ForEach(list.AddIfNotNull);
        }

        public static void RemoveIfExist<T>(this IList<T> list, T item)
        {
            if (list.Contains(item))
                list.Remove(item);
        }

        public static void EnsureNoZeroValue(this IList<int> list)
        {
            if (list.Contains(0))
                list.Remove(0);
        }
    }
}
