using System;
using System.Collections.Generic;
using SortingExtensions.Contracts;
using SortingExtensions.Extensions;

namespace SortingExtensions.Implementation.Sorters
{
    /// <summary>
    /// Shell sort - Move entries more than one position at a time by h-sorting the arrray.
    ///              an h-sorted array is h interleaved sorted subsequences
    /// 
    ///  Усовершенствованная сортировка вставками (Идея метода Шелла состоит в сравнении элементов, 
    ///  стоящих не только рядом, но и на определённом расстоянии друг от друга)
    ///  Не требует дополнительной памяти и работает с большими массивами
    /// 
    /// Calculate h:
    ///     1) 2^n - 1  1,3,7,15
    ///     2) 3n + 1   1,4,13,40,121,364
    ///     3) Sedgewick experimental 1,5,19,41,109,209,505,929, 2161, 3905...
    /// 
    /// + fast unless array size is huge
    /// + easy to code
    /// + can be implemented in hardware
    /// - not stable sort
    /// - nobody proved aravage
    /// 
    /// Example: [1,2,3,4,5,6,7,8,9], 3-sorting arrays: 1,4,7; 2,5,8; 3,6,9;
    ///          we sort with 7-sort
    ///     then we sort with 3-sort - its already partially sorted
    ///     then we sort with 1-sort - its already partially sorted
    /// 
    ///     1) Sort groups (a[0], a[13]), (a[1], a[14]) with insertion sort
    ///     2) Sort groups (a[0], a[4], a[8]), (a[1], a[5], a[9]) with insertion sort
    ///     3) Sort with insertion sort //h = 1
    /// 
    /// Performance: n ^ (3/2) compares - worst case
    /// Proposition: A g-sorted array remains g-sorted after h-sorting it. (easy to understand, but hard to prove)
    ///              If an array is both g-sorted and h-sorted, then it is also (g+h)-sorted.
    /// </summary>
    internal class ShellSort<TComparable> : ISorter<TComparable> where TComparable : IComparable<TComparable>
    {
        public void Sort(IList<TComparable> list, IComparer<TComparable> comparer)
        {
            int h = 1;
            while (h < list.Count/3) {
                h = 3 * h + 1; //1, 4, 13, 40, 121, 364, 1093, ...
            }

            while (h >= 1) //when h  = 1 it equals insertion sorting
            {
                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = i; j >= h && list[j].IsLessThan(list[j - h], comparer); j -= h)
                    {
                        list.Exchange(j, j - h);
                    }
                }
                h = h / 3;
            }
        }
    }

    internal class ShellSorterProvider : ISorterProvider
    {
        public ISorter<TComparable> GetSorter<TComparable>(string algorithmName) where TComparable : IComparable<TComparable>
        {
            return SingletonSorterProvider<ShellSort<TComparable>, TComparable>.GetSorter();
        }
    }
}
