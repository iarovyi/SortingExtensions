using System;
using System.Collections.Generic;
using SortingExtensions.Contracts;
using SortingExtensions.Extensions;

namespace SortingExtensions.Implementation.Sorters
{
    /*
     * Stable sort
     * 
    * 1) Divide the unsorted list into n sublists, each containing 1 element (a list of 1 element is considered sorted).
    * 2) Repeatedly merge sublists to produce new sorted sublists until there is only 1 sublist remaining. This will be the sorted list.
     * 
     * Merge sort is optimal with respect to compares, but its not optimal with repect
     * to memory
    * 
    * Требует дополнитеьлной памяти, но гарантированно сортирует за NLogN
    * и можно сортировать массивы из миллионов элементов(что нельзя сделать с выбором и вставками), но
    * вставка и слияние проце и будут работать быстрее на маленких массивах)
    * 
    * Нисходящая сортировка слиянием использует от 0,5NLgN до NlgN сравнений
    * не более 6NlgN обращений к массиву
     * 
     * 
     * 
    */

    internal abstract class MergeSort<TComparable> : ISorter<TComparable> where TComparable : IComparable<TComparable>
    {
        public abstract void Sort(IList<TComparable> list, IComparer<TComparable> comparer);

        protected static void Merge(IList<TComparable> list, TComparable[] aux, int lo, int mid, int hi)
        {
            // copy to aux[]
            // It can be eliminated - improvement
            for (int k = lo; k <= hi; k++)
            {
                aux[k] = list[k];
            }

            // merge back to a[]
            int i = lo, j = mid + 1;
            for (int k = lo; k <= hi; k++)
            {
                if (i > mid)                        list[k] = aux[j++];   //if the left half is over (this copying is unnecessary)
                else if (j > hi)                    list[k] = aux[i++];   //if the right half is over
                else if (aux[j].IsLessThan(aux[i])) list[k] = aux[j++];
                else                                list[k] = aux[i++];
            }
        }
    }

    /*
    * Нисходящяя сортировка слиянием (Рекурсивно делим пополам и сортируем)
    */
    internal class MergeUpBottomSort<TComparable> : MergeSort<TComparable> where TComparable : IComparable<TComparable>
    {
        public override void Sort(IList<TComparable> list, IComparer<TComparable> comparer)
        {
            var aux = new TComparable[list.Count];
            Sort(list, aux, 0, list.Count - 1);
        }

        internal static void Sort(IList<TComparable> list, TComparable[] aux, int lo, int hi)
        {
            /*Improvement + 20%:  add this and comment if (hi <= lo) return;
            //Merge sort has too much overhead for tiny subarrays (curoff to insertion sort for 7 items)
            if (hi <= lo + 7 - 1)
            {
                new InsertionSort().Sort(list, lo, hi);
                return;
            }*/

            if (hi <= lo) return;
            int mid = lo + (hi - lo) / 2;
            Sort(list, aux, lo, mid);
            Sort(list, aux, mid + 1, hi);

            //Improvement: helps for partially-orderred arrays
            //if biggerst item in first half less than smalest item in second half then it's already sorted
            //if (!list[mid + 1].IsLessThan(list[mid])) return;

            Merge(list, aux, lo, mid, hi);
        }
    }

    /*
    * Восходящяя сортировка слиянием
    * Нет рекурсии, поэтому надо меньше паняти на стеке
    */
    internal class MergeBottomUpSort<TComparable> : MergeSort<TComparable> where TComparable : IComparable<TComparable>
    {
        public override void Sort(IList<TComparable> list, IComparer<TComparable> comparer)
        {
            var aux = new TComparable[list.Count];
            for (int n = 1; n < list.Count; n = n + n)
            {
                for (int i = 0; i < list.Count - n; i += n + n)
                {
                    int lo = i;
                    int m = i + n - 1;
                    int hi = Math.Min(i + n + n - 1, list.Count - 1);
                    Merge(list, aux, lo, m, hi);
                }
            }
        }
    }

    internal static class MergeSorterProvider<TComparable> where TComparable : IComparable<TComparable>
    {
        internal static ISorter<TComparable> GetSorter(SortAlgorithm algorithm)
        {
            switch (algorithm)
            {
                case SortAlgorithm.MergeBottomUp: return SingletonSorterProvider<MergeBottomUpSort<TComparable>, TComparable>.GetSorter();
                case SortAlgorithm.MergeUpBottom: return SingletonSorterProvider<MergeUpBottomSort<TComparable>, TComparable>.GetSorter();
                default: throw new NotImplementedException("Merge sort type " + algorithm + " is not implemented");
            }
        }
    }
}
