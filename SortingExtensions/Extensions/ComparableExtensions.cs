using System;
using System.Collections.Generic;

namespace SortingExtensions.Extensions
{
    internal static class ComparableExtensions
    {
        internal static bool IsLessThan<TComparable>(this TComparable item1,
                                                     TComparable item2,
                                                     IComparer<TComparable> comparer) 
            where TComparable : IComparable<TComparable>
        {
            return comparer.Compare(item1, item2) < 0;
        }

        internal static bool IsBiggerThan<TComparable>(this TComparable item1,
                                                       TComparable item2,
                                                       IComparer<TComparable> comparer)
            where TComparable : IComparable<TComparable>
        {
            return comparer.Compare(item1, item2) > 0;
        }
    }
}
