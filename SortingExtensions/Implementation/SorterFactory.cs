using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SortingExtensions.Contracts;
using SortingExtensions.Implementation.Sorters;
using SortingExtensions.Implementation.Sorters.MergeSorts;
using SortingExtensions.Implementation.Sorters.QuickSorts;

namespace SortingExtensions.Implementation
{
    public static class SorterFactory
    {
        private static readonly ConcurrentDictionary<string, ISorterProvider> SorterProviders;

        static SorterFactory()
        {
            SorterProviders = new ConcurrentDictionary<string, ISorterProvider>(new Dictionary<string, ISorterProvider>()
            {
                {SortAlgorithm.Quick.ToString(),                                QuickSortProvider.Instance},
                {SortAlgorithm.QuickThreeWay.ToString(),                        QuickSortProvider.Instance},
                {SortAlgorithm.QuickWithCutoff.ToString(),                      QuickSortProvider.Instance},
                {SortAlgorithm.QuickWithMedianOfThree.ToString(),               QuickSortProvider.Instance},
                {SortAlgorithm.MergeBottomUp.ToString(),                        MergeSorterProvider.Instance},
                {SortAlgorithm.MergeUpBottom.ToString(),                        MergeSorterProvider.Instance},
                {SortAlgorithm.MergeUpBottomSortForPartiallySorted.ToString(),  MergeSorterProvider.Instance},
                {SortAlgorithm.MergeUpBottomSortWithCutoff.ToString(),          MergeSorterProvider.Instance},
                {SortAlgorithm.Bubble.ToString(),                               new BubbleSortProvider()},
                //{SortAlgorithm.RadixLsd.ToString(),                           new RadixSortProvider()},
                //{SortAlgorithm.RadixMsd.ToString(),                           new RadixSortProvider()},
                {SortAlgorithm.Heap.ToString(),                                 new HeapSorterProvider()},
                {SortAlgorithm.Insertion.ToString(),                            InsertionSorterProvider.Instance},
                {SortAlgorithm.Selection.ToString(),                            new SelectionSorterProvider()},
                {SortAlgorithm.Shell.ToString(),                                new ShellSorterProvider()}
            });
        }

        public static void AddSorterProvider(string sortAlgorithmName, ISorterProvider sorterProvider)
        {
            if (sorterProvider == null) {
                throw new ArgumentNullException("sorterProvider");
            }
            Contract.EndContractBlock();

            if(!SorterProviders.TryAdd(sortAlgorithmName, sorterProvider)) {
                throw new ArgumentException(string.Format("Sorter provider is already registered for {0} sort algorithm", sortAlgorithmName));
            }
        }

        internal static ISorter<TComparable> GetSorter<TComparable>(SortAlgorithm algorithm) where TComparable : IComparable<TComparable>
        {
            ISorterProvider sorterProvider = SorterProviders[algorithm.ToString()];
            if (sorterProvider == null) {
                throw new ArgumentException(string.Format("Sorter provider is not registered for {0} sort algorithm", algorithm));
            }

            return sorterProvider.GetSorter<TComparable>(algorithm.ToString());
        }
    }
}
