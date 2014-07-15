namespace SortingExtensions.Implementation.Sorters.QuickSorts
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Extensions;

    /// <summary>
    /// http://www.sorting-algorithms.com/quick-sort
    /// </summary>
    internal class QuickSort<TComparable> : BaseQuickSort<TComparable> where TComparable : IComparable<TComparable>
    {
        protected override void Sort(IList<TComparable> list, int lo, int hi, IComparer<TComparable> comparer)
        {
            Contract.Requires(list != null);
            Contract.Requires(comparer != null);
            Contract.Requires(lo >= 0 && lo <= list.Count);
            Contract.Requires(hi <= list.Count);

            if (hi <= lo) return;

            int j = Partition(list, lo, hi, comparer);

            Sort(list, lo, j - 1, comparer);
            Sort(list, j + 1, hi, comparer);
        }

        /// <summary>
        /// Partition the subarray and return partition index.  (list[lo..j-1] &lt;= list[j] &lt;= list[j+1..hi])
        /// Algorithm:
        /// 1) Select first element(lo) as pivot number
        /// 2) Go from left(i) and go from right(j) until i meet j.
        ///     2.1)  go from left to right(i) until we find element bigger then pivot
        ///     2.2)  go from right to left(j) until we find element smaller then pivot
        ///     2.3)  exchange them
        /// 3) Exchange first element and a[j]
        /// </summary>
        /// <returns>partition inex of subarray so all elements before j smaller then all elemens after j</returns>
        public static int Partition(IList<TComparable> list, int lo, int hi, IComparer<TComparable> comparer)
        {
            Contract.Requires(list != null);
            Contract.Requires(comparer != null);
            Contract.Requires(lo >= 0 && lo <= list.Count);
            Contract.Requires(hi >= 0 && hi <= list.Count);
            Contract.Requires(lo <= hi);

            TComparable pivotValue = list[lo];
            int i = lo,
                j = hi + 1;
            while (true)
            {
                while (list[++i].IsLessThan(pivotValue, comparer)) // find item on lo to swap
                    if (i == hi) break;

                while (pivotValue.IsLessThan(list[--j], comparer)) // find item on hi to swap
                    if (j == lo) break;

                if (i >= j) break;                                 // check if pointers cross

                list.Exchange(i, j);
            }

            list.Exchange(lo, j);                                  // put partitioning item 'pivotValue' at list[j]
            return j;                                              // now, list[lo .. j-1] <= list[j] <= list[j+1 .. hi]
        }
    }
}
