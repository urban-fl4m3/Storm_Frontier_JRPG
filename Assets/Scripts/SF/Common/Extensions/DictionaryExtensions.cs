using System.Collections.Generic;

namespace SF.Common.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dict, TKey key)
        {
            return !dict.ContainsKey(key) ? default : dict[key];
        }
    }
}