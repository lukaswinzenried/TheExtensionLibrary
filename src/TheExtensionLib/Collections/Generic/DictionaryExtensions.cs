using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Collections.Generic
{
    public static class DictionaryExtensions
    {
        public static TV GetValueOrDefault<TK, TV>(this IDictionary<TK, TV> dict, TK key)
        {
            return dict.GetValueOrDefault(key, default(TV));
        }

        public static TV GetValueOrDefault<TK, TV>(this IDictionary<TK, TV> dict, TK key, TV @default)
        {
            return dict.ContainsKey(key) ? dict[key] : @default;
        }

        public static TV GetValueOrDefaultByKey<TK, TV>(this IDictionary<TK, TV> dict, TK key, TK defaultKey)
        {
            return dict.ContainsKey(key) ? dict[key] : dict[defaultKey];
        }

        public static T GetItemOrNew<T>(this IDictionary<string, T> dic, string key) where T : new()
        {
            if (dic.ContainsKey(key))
            {
                return dic[key];
            }
            return new T();
        }

        public static void AddOrReplace(this Dictionary<string, string> dic, string key, string value)
        {
            // add or overwrite
            if (!dic.ContainsKey(key))
                dic.Add(key, value);
            else
                dic[key] = value;
        }
    }
}
