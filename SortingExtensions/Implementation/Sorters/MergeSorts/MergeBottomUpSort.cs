using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace SortingExtensions.Implementation.Sorters.MergeSorts
{
    /// <summary>
    /// Bottom-up mergesort is without recursion so it requires less stack memory.
    /// 
    /// Performance: uses between 1/2 N lg N and N lg N compares and at most 6 N lg N array accesses to sort any array of length N.
    /// http://algs4.cs.princeton.edu/22mergesort/
    /// </summary>
    internal class MergeBottomUpSort<TComparable> : MergeSort<TComparable> where TComparable : IComparable<TComparable>
    {
        public override void Sort(IList<TComparable> list, IComparer<TComparable> comparer)
        {
            Contract.Requires(list != null);
            Contract.Requires(comparer != null);
            Contract.Ensures(list.IsSorted());

            var aux = new TComparable[list.Count];
            for (int n = 1; n < list.Count; n = n + n)
            {
                for (int i = 0; i < list.Count - n; i += n + n)
                {
                    int lo = i,
                        m = i + n - 1,
                        hi = Math.Min(i + n + n - 1, list.Count - 1);

                    Merge(list, aux, lo, m, hi, comparer);
                }
            }
        }
    }
}
