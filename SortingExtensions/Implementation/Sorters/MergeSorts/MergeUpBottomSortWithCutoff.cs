namespace SortingExtensions.Implementation.Sorters.MergeSorts
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Contracts;

    /// <summary>
    /// Top-bottom merge sort with improvement for tiny arrays.
    /// Merge sort has too much overhead for tiny subarrays so this improvement increase performance by 20% for small arrays.
    /// </summary>
    internal class MergeUpBottomSortWithCutoff<TComparable> : MergeUpBottomSort<TComparable> where TComparable : IComparable<TComparable>
    {
        private const int Cutoff = 7;
        private static readonly InsertionSort<TComparable> InsertionSort;

        static MergeUpBottomSortWithCutoff()
        {
            InsertionSort = (InsertionSort<TComparable>)SorterFactory.GetSorter<TComparable>(SortAlgorithm.Insertion);
        }

        internal override void Sort(IList<TComparable> list, TComparable[] aux, int lo, int hi, IComparer<TComparable> comparer)
        {
            Contract.Requires(list != null);
            Contract.Requires(aux != null);
            Contract.Requires(lo >= 0);
            Contract.Requires(hi < list.Count);

            #region Improvement: curoff to insertion sort for 7 items
            if (hi <= lo + Cutoff - 1)
            {
                InsertionSort.Sort(list, lo, hi, comparer);
                return;
            }
            #endregion

            if (hi <= lo) return;
            int mid = lo + (hi - lo) / 2;
            Sort(list, aux, lo, mid, comparer);
            Sort(list, aux, mid + 1, hi, comparer);

            Merge(list, aux, lo, mid, hi, comparer);
        }
    }
}
