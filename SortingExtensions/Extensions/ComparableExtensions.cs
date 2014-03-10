using System;
using System.Collections.Generic;

namespace SortingExtensions.Extensions
{
    internal static class ComparableExtensions
    {
        internal static void Exchange<TComparable>(this IList<TComparable> list, int index1, int index2) where TComparable : IComparable<TComparable>
        {
            TComparable temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }

        internal static bool IsLessThan<TComparable>(this TComparable item1, TComparable item2) where TComparable : IComparable<TComparable>
        {
            return item1.CompareTo(item2) < 0;
        }

        internal static bool IsBiggerThan<TComparable>(this TComparable item1, TComparable item2) where TComparable : IComparable<TComparable>
        {
            return item1.CompareTo(item2) > 0;
        }
    }
}
