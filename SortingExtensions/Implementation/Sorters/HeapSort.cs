using System;
using System.Collections.Generic;
using SortingExtensions.Contracts;

namespace SortingExtensions.Implementation.Sorters
{
    /// <summary>
    /// Heap sort - can be treated as improved bubble sort where element pops (min-heap)/ sink (max-heap) by many ways
    ///             http://upload.wikimedia.org/wikipedia/commons/4/4d/Heapsort-example.gif
    /// 
    /// + sorts inplace
    /// + do not require additional memory
    /// - is rarely used because of bad effectiveness of processor cache (adjacent items rarely compared)
    /// 
    /// Algorithm: 1) build heap
    ///            2) A sorted array is created by repeatedly removing the largest element from the 
    ///               heap(heap part of array) to the sorted part of array and reconstructing the heap
    ///            3) all element are removed from the heap -> array is sorted
    /// </summary>
    internal class HeapSort<TComparable> : ISorter<TComparable> where TComparable : IComparable<TComparable>
    {
        public void Sort(IList<TComparable> list, IComparer<TComparable> comparer)
        {
            int N = list.Count;
            
            for (int i = list.Count / 2; i >= 1; i--) { // build pyramid
                Sink(list, i, N);
            }

            while (N > 1) {
                list.ExchangeByPosition(1, N--);        // move max heap's element into sorted part of array
                Sink(list, 1, N);                       // reestablish heap (max element will be again on array[1])
            }
        }

        private static void Sink(IList<TComparable> list, int k, int N)
        {
            while (2 * k <= N) {
                int j = 2 * k;
                if (j < N && IsLessByPosition(list, j, j + 1)) {
                    j++;
                }
                if (!IsLessByPosition(list, k, j)) {
                    break;
                }
                list.ExchangeByPosition(k, j);
                k = j;
            }
        }

        private static bool IsLessByPosition(IList<TComparable> list, int i, int j)
        {
            return list[i - 1].CompareTo(list[j - 1]) < 0;
        }
    }

    internal class HeapSorterProvider : ISorterProvider
    {
        public ISorter<TComparable> GetSorter<TComparable>(string algorithmName) where TComparable : IComparable<TComparable>
        {
            return SingletonSorterProvider<HeapSort<TComparable>, TComparable>.GetSorter();
        }
    }
}
