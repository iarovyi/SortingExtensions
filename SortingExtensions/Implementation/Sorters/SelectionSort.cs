using System;
using System.Collections.Generic;
using SortingExtensions.Contracts;
using SortingExtensions.Extensions;

namespace SortingExtensions.Implementation.Sorters
{
    /// <summary>
    /// Selection sort - First, find the smallest item in the array and exchange it with the first entry
    ///     (itself if the first entry is already the smallest). Then, find the next smallest item and 
    ///     exchange it with the second entry. Continue in this way until the entire array is sorted.
    /// 
    /// - not stable sort
    /// - Quadratic time, event if input is sorted
    /// + data movement is minumum (linear number of exchanges)
    /// 
    /// Performance: N ^ 2 / 2 comparisons, N excahnges
    /// </summary>
    internal class SelectionSorter<TComparable> : ISorter<TComparable> where TComparable : IComparable<TComparable>
    {
        public void Sort(IList<TComparable> list, IComparer<TComparable> comparer)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[j].IsLessThan(list[minIndex], comparer))
                    {
                        minIndex = j;
                    }
                }

                list.Exchange(minIndex, i);
            }
        }
    }

    internal class SelectionSorterProvider : ISorterProvider
    {
        public ISorter<TComparable> GetSorter<TComparable>(string algorithmName) where TComparable : IComparable<TComparable>
        {
            return SingletonSorterProvider<SelectionSorter<TComparable>, TComparable>.GetSorter();
        }
    }
}
