using System;
using System.Collections.Generic;
using SortingExtensions.Contracts;
using SortingExtensions.Extensions;

namespace SortingExtensions.Implementation.Sorters
{
    internal class InsertionSort<TComparable> : ISorter<TComparable> where TComparable : IComparable<TComparable>
    {
        /*
         * Stable sort
         * 
            The algorithm that people often use to sort bridge hands is to consider
            the cards one at a time, inserting each into its proper place among those already
            considered (keeping them sorted).
         * N^2/4 compares and N^2/4 exchanges on average (on randomly sorted array)
         * N^2/2 compares and N^2/2 exchanges on worst case (array is sorted in descending order)
         * 
         * эфективна на малых наборах(потому что перестановки только для соседних элементов), устойчива
         * + best case (sorted in ascending order) it makes N-1 compares and 0 exchanges
         * For partially-sorted arrays it runs in linear time
         */
        public void Sort(IList<TComparable> list, IComparer<TComparable> comparer)
        {
            /*for (int i = 0; i < list.Count; i++)
            {
                for (int j = i; j > 0 && list[j].IsLessThan(list[j - 1]); j--)
                {
                    list.Exchange(j, j - 1);
                }
            }*/
            Sort(list, 0, list.Count - 1, comparer);
        }

        internal void Sort(IList<TComparable> list, int lo, int hi)
        {
            Sort(list, 0, list.Count - 1, Comparer<TComparable>.Default);
        }

        internal void Sort<TComparable>(IList<TComparable> list, int lo, int hi, IComparer<TComparable> comparer)
            where TComparable : IComparable<TComparable>
        {
            for (int i = lo/*0*/; i <= hi/*< list.Count*/; i++)
            {
                for (int j = i; j > lo/*0*/ && list[j].IsLessThan(list[j - 1]); j--)
                {
                    list.Exchange(j, j - 1);
                }
            }
        }
    }

    internal static class InsertionSorterProvider<TComparable> where TComparable : IComparable<TComparable>
    {
        internal static ISorter<TComparable> GetSorter(SortAlgorithm algorithm)
        {
            return SingletonSorterProvider<InsertionSort<TComparable>, TComparable>.GetSorter();
        }
    }
}
