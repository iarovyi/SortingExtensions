﻿using System;
using System.Collections.Generic;
using SortingExtensions.Contracts;
using SortingExtensions.Extensions;

namespace SortingExtensions.Implementation.Sorters
{
    /* First, find the smallest item in the array and exchange it with the first entry (itself if the first entry
           is already the smallest). Then, find the next smallest item and exchange it with the second
           entry. Continue in this way until the entire array is sorted.
         N ^ 2 / 2 comparisons
         N excahnges
         - Quadratic time, event if input is sorted
     *  + data movement is minumum (linear number of exchanges)
         * 
         * Not stable sort
         */
    internal class SelectionSorter<TComparable> : ISorter<TComparable> where TComparable : IComparable<TComparable>
    {
        public void Sort(IList<TComparable> list, IComparer<TComparable> comparer)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[j].IsLessThan(list[minIndex]))
                    {
                        minIndex = j;
                    }
                }

                list.Exchange(minIndex, i);
            }
        }
    }

    internal static class SelectionSorterProvider<TComparable> where TComparable : IComparable<TComparable>
    {
        internal static ISorter<TComparable> GetSorter(SortAlgorithm algorithm)
        {
            return SingletonSorterProvider<SelectionSorter<TComparable>, TComparable>.GetSorter();
        }
    }
}
