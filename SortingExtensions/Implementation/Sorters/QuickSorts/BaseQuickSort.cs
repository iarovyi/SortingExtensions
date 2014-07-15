namespace SortingExtensions.Implementation.Sorters.QuickSorts
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Contracts;

    internal abstract class BaseQuickSort<TComparable> : ISorter<TComparable> where TComparable : IComparable<TComparable>
    {
        public void Sort(IList<TComparable> list, IComparer<TComparable> comparer)
        {
            if (list == null)
                throw new ArgumentNullException("list");
            if (comparer == null)
                throw new ArgumentNullException("comparer");
            Contract.Ensures(list.IsSorted(0, list.Count - 1, comparer));
            Contract.EndContractBlock();

            //Shuffle needed for performance guarantee because in worst case number of compares is quadratic (0.5N^2)
            //Randomized quicksort with 3-way paritioning reduces running time from liearithimic to linear in broad class of applications.
            list.Shuffle();

            Sort(list, 0, list.Count - 1, comparer);
        }

        protected abstract void Sort(IList<TComparable> list, int lo, int hi, IComparer<TComparable> comparer);
    }

    internal class QuickSortProvider : ISorterProvider
    {
        public static readonly QuickSortProvider Instance;

        static QuickSortProvider()
        {
            Instance = new QuickSortProvider();
        }

        public ISorter<TComparable> GetSorter<TComparable>(string sorterName) where TComparable : IComparable<TComparable>
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(sorterName));
            Contract.Ensures(Contract.Result<ISorter<TComparable>>() != null);

            return sorterName == SortAlgorithm.Quick.ToString() ? SingletonSorterProvider<QuickSort<TComparable>, TComparable>.GetSorter()
                 : sorterName == SortAlgorithm.QuickThreeWay.ToString() ? SingletonSorterProvider<QuickThreeWaySort<TComparable>, TComparable>.GetSorter()
                 : sorterName == SortAlgorithm.QuickWithCutoff.ToString() ? SingletonSorterProvider<QuickSortWithCutoff<TComparable>, TComparable>.GetSorter()
                 : sorterName == SortAlgorithm.QuickWithMedianOfThree.ToString() ? SingletonSorterProvider<QuickSortMedianOfThree<TComparable>, TComparable>.GetSorter()
                 : null;
        }
    }
}
