using System;
using System.Collections.Generic;
using SortingExtensions.Implementation.Sorters.QuickSorts;

namespace SortingExtensions.Implementation
{
    internal static class SelectHelper<TComparable> where TComparable : IComparable<TComparable>
    {
        /// <summary>
        /// Select n-th element of list in sort order
        /// Performance: Takes linear time on avarage. 0.5N^2 compares in worst case.
        /// </summary>
        /// <param name="list">comparable items list</param>
        /// <param name="n">inder of item in sorted list</param>
        /// <param name="comparer">comparer to sort items</param>
        /// <returns>n-th item of list in sort order</returns>
        internal static TComparable Select(IList<TComparable> list, int n, IComparer<TComparable> comparer)
        {
            list.Shuffle();
            int lo = 0, hi = list.Count - 1;
            while (hi > lo)
            {
                int j = QuickSort<TComparable>.Partition(list, lo, hi, comparer);
                if      (j < n) lo = j + 1;
                else if (j < n) hi = j - 1;
                else            return list[n];
            }

            return list[n];
        }
    }
}
