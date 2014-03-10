using System;
using SortingExtensions.Contracts;
using SortingExtensions.Implementation.Sorters;

namespace SortingExtensions.Implementation
{
    internal static class SorterFactory<TComparable> where TComparable : IComparable<TComparable>
    {
        internal static ISorter<TComparable> GetSorter(SortCase condition)
        {
            //TODO implement
            throw new NotImplementedException();
        }

        //TODO add ability to add sorting algorithms from outside
        //TODO add many properties to ISorter
        internal static ISorter<TComparable> GetSorter(SortAlgorithm algorithm)
        {
            var sorter = algorithm == SortAlgorithm.Quick ? QuickSortProvider<TComparable>.GetSorter(algorithm)
                       : algorithm == SortAlgorithm.MergeBottomUp ? MergeSorterProvider<TComparable>.GetSorter(algorithm)
                       : algorithm == SortAlgorithm.MergeUpBottom ? MergeSorterProvider<TComparable>.GetSorter(algorithm)
                       : algorithm == SortAlgorithm.Bubble ? null
                       : algorithm == SortAlgorithm.Heap ? HeapSorterProvider<TComparable>.GetSorter(algorithm)
                       : algorithm == SortAlgorithm.Insertion ? InsertionSorterProvider<TComparable>.GetSorter(algorithm)
                       : algorithm == SortAlgorithm.Selection ? SelectionSorterProvider<TComparable>.GetSorter(algorithm)
                       : algorithm == SortAlgorithm.Shell ? ShellSorterProvider<TComparable>.GetSorter(algorithm)
                       : null;

            if (sorter == null)
            {
                throw new NotImplementedException("Sorter is not implemented for " + algorithm + " algorithm");
            }

            return sorter;
        }
    }
}
