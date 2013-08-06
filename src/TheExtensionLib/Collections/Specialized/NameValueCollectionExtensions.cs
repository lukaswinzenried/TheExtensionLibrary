using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Collections.Specialized
{
    public static class NameValueCollectionExtensions
    {
        public static string ToQueryString(this NameValueCollection nvc, bool startWithQuerySeparator = false)
        {
            var sb = new StringBuilder();
            foreach (var key in nvc.AllKeys)
                sb.AppendFormat("{0}={1}&", key, nvc[key]);

            var queryString = sb.ToString();
            if (queryString.Length > 0)
            {
                queryString = queryString.Remove(queryString.Length - 1);
                if (startWithQuerySeparator)
                    queryString = queryString.Insert(0, "?");
            }
 
            return queryString;
        }

        public static NameValueCollection SetValue(this NameValueCollection nvc, string key, string value)
        {
            nvc[key] = value;
            return nvc;
        }

        public static NameValueCollection RemoveValue(this NameValueCollection nvc, string key)
        {
            nvc.Remove(key);
            return nvc;
        }

        public static void AddIfNotNullOrEmpty(this NameValueCollection nvc, string name, string value)
        {
            if (!string.IsNullOrEmpty(value))
                nvc.Add(name, value);
        }

        public static NameValueCollection Clone(this NameValueCollection nvc)
        {
            return new NameValueCollection(nvc);
        }

        /// <summary>
        /// Partial clone with whitelisted params (default mode) / without blacklisted params
        /// </summary>
        /// <param name="nvc"></param>
        /// <param name="keys"></param>
        /// <param name="whitelistMode"></param>
        /// <returns></returns>
        public static NameValueCollection ClonePartial(this NameValueCollection nvc, string[] keys, bool whitelistMode = true)
        {
            var q = new NameValueCollection();
            if (whitelistMode)
                nvc.AllKeys.Intersect(keys).ForEach(k => q.AddIfNotNullOrEmpty(k, nvc[k]));
            else
                nvc.AllKeys.Except(keys).ForEach(k => q.AddIfNotNullOrEmpty(k, nvc[k]));
            return q;
        }
    }
}
