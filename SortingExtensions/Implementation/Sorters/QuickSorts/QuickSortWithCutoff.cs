namespace SortingExtensions.Implementation.Sorters.QuickSorts
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Contracts;

    /// <summary>
    /// Quick sort has too much overhead for small arrays
    /// </summary>
    internal class QuickSortWithCutoff<TComparable> : QuickSort<TComparable> where TComparable : IComparable<TComparable>
    {
        private const int Cutoff = 10;
        private static readonly InsertionSort<TComparable> InsertionSort;

        static QuickSortWithCutoff()
        {
            InsertionSort = (InsertionSort<TComparable>)SorterFactory.GetSorter<TComparable>(SortAlgorithm.Insertion);
        }

        protected override void Sort(IList<TComparable> list, int lo, int hi, IComparer<TComparable> comparer)
        {
            Contract.Requires(list != null);
            Contract.Requires(comparer != null);
            Contract.Requires(lo >= 0 && lo <= list.Count);
            Contract.Requires(hi <= list.Count);

            if (hi <= lo) return;

            #region Improvement
            if (hi <= lo + Cutoff - 1)
            {
                InsertionSort.Sort(list, lo, hi, comparer);
                return;
            }
            #endregion

            int j = Partition(list, lo, hi, comparer);

            Sort(list, lo, j - 1, comparer);
            Sort(list, j + 1, hi, comparer);
        }
    }
}
