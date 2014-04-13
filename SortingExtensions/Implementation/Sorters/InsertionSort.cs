using System;
using System.Collections.Generic;
using SortingExtensions.Contracts;
using SortingExtensions.Extensions;

namespace SortingExtensions.Implementation.Sorters
{
    /// <summary>
    /// Insertion sort - The algorithm that people often use to sort bridge hands is to consider
    ///                  the cards one at a time, inserting each into its proper place among those already
    ///                  considered (keeping them sorted).
    /// 
    /// + stable sort
    /// - effective only for small arrays because exchanges are done on adjacent items
    /// 
    /// Performance: N^2/4 compares and N^2/4 exchanges on average (on randomly sorted array)
    ///              N^2/2 compares and N^2/2 exchanges on worst case (array is sorted in descending order)
    ///              Best case (sorted in ascending order) it makes N-1 compares and 0 exchanges
    ///              For partially-sorted arrays it runs in linear time
    /// </summary>
    internal class InsertionSort<TComparable> : ISorter<TComparable> where TComparable : IComparable<TComparable>
    {
        public void Sort(IList<TComparable> list, IComparer<TComparable> comparer)
        {
            Sort(list, 0, list.Count - 1, comparer);
        }

        internal void Sort(IList<TComparable> list, int lo, int hi)
        {
            Sort(list, 0, list.Count - 1, Comparer<TComparable>.Default);
        }

        internal void Sort(IList<TComparable> list, int lo, int hi, IComparer<TComparable> comparer)
        {
            for (int i = lo; i <= hi; i++)
            {
                for (int j = i; j > lo && list[j].IsLessThan(list[j - 1], comparer); j--)
                {
                    list.Exchange(j, j - 1);
                }
            }
        }
    }

    internal class InsertionSorterProvider : ISorterProvider
    {
        public static readonly InsertionSorterProvider Instance;

        static InsertionSorterProvider()
        {
            Instance = new InsertionSorterProvider();
        }

        public ISorter<TComparable> GetSorter<TComparable>(string algorithmName) where TComparable : IComparable<TComparable>
        {
            return SingletonSorterProvider<InsertionSort<TComparable>, TComparable>.GetSorter();
        }
    }
}
