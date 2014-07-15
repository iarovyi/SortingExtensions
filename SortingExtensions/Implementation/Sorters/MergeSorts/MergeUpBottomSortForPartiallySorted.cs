namespace SortingExtensions.Implementation.Sorters.MergeSorts
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Extensions;

    /// <summary>
    /// Top-bottom merge sort with improvement for sorting partially-orderred arrays.
    /// </summary>
    internal class MergeUpBottomSortForPartiallySorted<TComparable> : MergeUpBottomSort<TComparable> 
        where TComparable : IComparable<TComparable>
    {
        internal override void Sort(IList<TComparable> list, TComparable[] aux, int lo, int hi, IComparer<TComparable> comparer)
        {
            Contract.Requires(list != null);
            Contract.Requires(aux != null);
            Contract.Requires(lo >= 0);
            Contract.Requires(hi < list.Count);

            if (hi <= lo) return;
            int mid = lo + (hi - lo) / 2;
            Sort(list, aux, lo, mid, comparer);
            Sort(list, aux, mid + 1, hi, comparer);

            #region Improvement: if biggerst item in first half less than smalest item in second half then it's already sorted
            if (!list[mid + 1].IsLessThan(list[mid], comparer)) {
                return;
            }
            #endregion

            Merge(list, aux, lo, mid, hi, comparer);
        }
    }
}
