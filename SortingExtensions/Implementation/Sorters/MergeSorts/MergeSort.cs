namespace SortingExtensions.Implementation.Sorters.MergeSorts
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Contracts;
    using Extensions;

    /// <summary>
    /// Merge sort - Merge sort is optimal with respect to compares, but its not optimal with repect to memory
    /// 
    /// + stable sort
    /// 
    /// Algorithm:
    ///     1) Divide the unsorted list into n sublists, each containing 1 element (a list of 1 element is considered sorted).
    ///     2) Repeatedly merge sublists to produce new sorted sublists until there is only 1 sublist remaining. This will be the sorted list.
    /// 
    /// Performance: up-bottom merge sort takes 0,5NLgN - NlgN compares and no more than 6NlgN interactions with array
    ///              It requres additional memory but it guarantees sorts as NLogN and it can sort arrays 
    ///              of millions of items (what can't be done with insertion or selection sort), but
    ///              insertion and selction sort are easier and works faster on small arrays.
    /// </summary>
    internal abstract class MergeSort<TComparable> : ISorter<TComparable> where TComparable : IComparable<TComparable>
    {
        public abstract void Sort(IList<TComparable> list, IComparer<TComparable> comparer);

        protected static void Merge(IList<TComparable> list, TComparable[] aux, int lo, int mid, int hi, IComparer<TComparable> comparer)
        {
            Contract.Requires(list != null);
            Contract.Requires(aux != null);
            Contract.Requires(comparer != null);

            // copy to aux[]
            // It can be eliminated - improvement
            for (int k = lo; k <= hi; k++)
            {
                aux[k] = list[k];
            }

            // merge back to a[]
            int i = lo, j = mid + 1;
            for (int k = lo; k <= hi; k++)
            {
                if (i > mid)                                  list[k] = aux[j++];   //if the left half is over (this copying is unnecessary)
                else if (j > hi)                              list[k] = aux[i++];   //if the right half is over
                else if (aux[j].IsLessThan(aux[i], comparer)) list[k] = aux[j++];
                else                                          list[k] = aux[i++];
            }
        }
    }

    internal class MergeSorterProvider : ISorterProvider 
    {
        public static readonly MergeSorterProvider Instance;

        static MergeSorterProvider()
        {
            Instance = new MergeSorterProvider();
        }

        public ISorter<TComparable> GetSorter<TComparable>(string algorithmName) where TComparable : IComparable<TComparable>
        {
            var sorter = SortAlgorithm.MergeBottomUp.ToString() == algorithmName ? SingletonSorterProvider<MergeBottomUpSort<TComparable>, TComparable>.GetSorter()
                       : SortAlgorithm.MergeUpBottom.ToString() == algorithmName ? SingletonSorterProvider<MergeUpBottomSort<TComparable>, TComparable>.GetSorter()
                       : SortAlgorithm.MergeUpBottomSortForPartiallySorted.ToString() == algorithmName ? SingletonSorterProvider<MergeUpBottomSortForPartiallySorted<TComparable>, TComparable>.GetSorter()
                       : SortAlgorithm.MergeUpBottomSortWithCutoff.ToString() == algorithmName ? SingletonSorterProvider<MergeUpBottomSortWithCutoff<TComparable>, TComparable>.GetSorter()
                       : null;

            if (sorter == null) {
                throw new NotImplementedException("Merge sort type " + algorithmName + " is not implemented");
            }

            return sorter;
        }
    }
}
