using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace SortingExtensions.Implementation.Sorters.MergeSorts
{
    /// <summary>
    /// Top-down mergesort (recursively divide and sort)
    /// </summary>
    internal class MergeUpBottomSort<TComparable> : MergeSort<TComparable> where TComparable : IComparable<TComparable>
    {
        public override void Sort(IList<TComparable> list, IComparer<TComparable> comparer)
        {
            if (list == null)
                throw new ArgumentNullException("list");
            if (comparer == null)
                throw new ArgumentNullException("comparer");
            Contract.Ensures(list.IsSorted());
            Contract.EndContractBlock();

            var aux = new TComparable[list.Count];
            Sort(list, aux, 0, list.Count - 1, comparer);
        }

        internal virtual void Sort(IList<TComparable> list, TComparable[] aux, int lo, int hi, IComparer<TComparable> comparer)
        {
            Contract.Requires(list != null);
            Contract.Requires(aux != null);
            Contract.Requires(lo >= 0);
            Contract.Requires(hi < list.Count);

            if (hi <= lo) return;
            int mid = lo + (hi - lo) / 2;
            Sort(list, aux, lo, mid, comparer);
            Sort(list, aux, mid + 1, hi, comparer);

            Merge(list, aux, lo, mid, hi, comparer);
        }
    }
}
