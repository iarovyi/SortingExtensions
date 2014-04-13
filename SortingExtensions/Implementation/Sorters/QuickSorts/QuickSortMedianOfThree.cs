using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SortingExtensions.Extensions;

namespace SortingExtensions.Implementation.Sorters.QuickSorts
{
    /// <summary>
    /// Quick sort with median of three improvement can improve performance by 10% 
    /// because it slightly decrese number of compares, increases number of exchanges.
    /// Best choise for pivot item is median.
    /// http://stackoverflow.com/questions/7559608/median-of-three-values-strategy
    /// </summary>
    internal class QuickSortMedianOfThree<TComparable> : QuickSort<TComparable> where TComparable : IComparable<TComparable>
    {
        protected override void Sort(IList<TComparable> list, int lo, int hi, IComparer<TComparable> comparer)
        {
            Contract.Requires(list != null);
            Contract.Requires(comparer != null);
            Contract.Requires(lo >= 0 && lo <= list.Count);
            Contract.Requires(hi <= list.Count);

            if (hi <= lo) return;

            #region Improvement
            int center = lo + (hi - lo) / 2, //(left + right) / 2
                m = MedianOf3(list, lo, center, hi, comparer);
            list.Exchange(lo, m);
            #endregion

            int j = Partition(list, lo, hi, comparer);

            Sort(list, lo, j - 1, comparer);
            Sort(list, j + 1, hi, comparer);
        }

        /// <summary>
        /// Select leftmost, middle and rightmost element, order them to the left partition, pivot and right partition.
        /// Use the pivot in the same fashion as regular quicksort.
        /// </summary>
        /// <returns>median index</returns>
        private static int MedianOf3(IList<TComparable> list, int left, int center, int right, IComparer<TComparable> comparer)
        {
            Contract.Requires(list != null);
            Contract.Requires(list.Count >= right);
            Contract.Requires(center >= left);
            Contract.Requires(right >= center);
            Contract.Ensures(comparer.Compare(list[left], list[right]) <= 0);
            Contract.Ensures(Contract.Result<int>() == right - 1);

            if (list[left].IsBiggerThan(list[center], comparer))  // order left & center
                list.Exchange(left, center);

            if (list[left].IsBiggerThan(list[right], comparer))   // order left & right
                list.Exchange(left, right);

            if (list[center].IsBiggerThan(list[right], comparer)) // order center & right
                list.Exchange(center, right);

            list.Exchange(center, right - 1);                     // put pivot on right
            return right - 1;                                     // return median index
        }
    }
}
