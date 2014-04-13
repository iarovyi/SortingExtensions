using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SortingExtensions.Extensions
{
    internal static class DictionaryExtensions
    {
        internal static IReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return new ReadOnlyDictionary<TKey, TValue>(dictionary);
        } 
    }
}
