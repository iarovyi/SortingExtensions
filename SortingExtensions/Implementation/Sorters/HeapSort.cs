using System;
using System.Collections.Generic;
using SortingExtensions.Contracts;

namespace SortingExtensions.Implementation.Sorters
{
    /*
         * Может рассматриваться как усовершенствованная сортировка пузырьком, в которой элемент всплывает (min-heap) / тонет (max-heap) по многим путям.
         * http://upload.wikimedia.org/wikipedia/commons/4/4d/Heapsort-example.gif
         * 1) build heap
         * 2) A sorted array is created by repeatedly removing the largest element from the heap(heap part of array) to the sorted part 
         *    of array and reconstructing the heap
         * 3) all element are removed from the heap -> array is sorted
         *  + сортирует на месте, не требует дополнительной памяти
         *  - редко используется из-за ухудшения производительности кеша (элементы массива редко сравниваются с соседними элементами) 
         */
    internal class HeapSort<TComparable> : ISorter<TComparable> where TComparable : IComparable<TComparable>
    {
        public void Sort(IList<TComparable> list, IComparer<TComparable> comparer)
        {
            int N = list.Count;
            //Посроение пирамиды
            for (int i = list.Count / 2; i >= 1; i--)
            {
                sink(list, i, N);
            }

            while (N > 1)
            {
                //Переместить максимальный элемент хипа в отсортированную часть массива в конце массива
                exch_by_position(list, 1, N--);
                //Восстановить хип (максимальный элемент снова будет array[1])
                sink(list, 1, N);
            }
        }

        private static void sink(IList<TComparable> list, int k, int N)
        {
            while (2 * k <= N)
            {
                int j = 2 * k;
                if (j < N && less_by_position(list, j, j + 1)) j++;
                if (!less_by_position(list, k, j)) break;
                exch_by_position(list, k, j);
                k = j;
            }
        }

        private static bool less_by_position(IList<TComparable> list, int i, int j)
        {
            return list[i - 1].CompareTo(list[j - 1]) < 0;
        }

        private static void exch_by_position(IList<TComparable> list, int i, int j)
        {
            TComparable swap = list[i - 1];
            list[i - 1] = list[j - 1];
            list[j - 1] = swap;
        }
    }

    internal static class HeapSorterProvider<TComparable> where TComparable : IComparable<TComparable>
    {
        internal static ISorter<TComparable> GetSorter(SortAlgorithm algorithm)
        {
            return SingletonSorterProvider<HeapSort<TComparable>, TComparable>.GetSorter();
        }
    }
}
