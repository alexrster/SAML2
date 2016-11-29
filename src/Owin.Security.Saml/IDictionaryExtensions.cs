﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Owin.Security.Saml
{
    public static class DictionaryExtensions
    {
        public static NameValueCollection ToNameValueCollection<TKey, TValue>(this IDictionary<TKey, TValue> value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            var nvc = new NameValueCollection(value.Count);
            foreach (var item in value)
                nvc.Add(item.Key.ToString(), item.Value.ToString());
            return nvc;
        }

        public static string ToDelimitedString<TKey, TValue>(this IDictionary<TKey, TValue> value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            return string.Join("&", value.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value.ToString())}"));
        }

        public static IEnumerable<KeyValuePair<string,string>> FromDelimitedString(this string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            return value.Split('&')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(kvp =>
                {
                    var split = kvp.Split('=');
                    return new KeyValuePair<string, string>(split[0], split.Length > 1 ? Uri.UnescapeDataString(split[1]) : string.Empty);
                });
        }
    }
}
