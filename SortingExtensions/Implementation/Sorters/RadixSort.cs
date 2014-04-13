using System;
using System.Collections.Generic;
using SortingExtensions.Contracts;

namespace SortingExtensions.Implementation.Sorters
{
    /// <summary>
    /// There are two types of radix sorting:
    ///   - Least significant digit (LSD) radix sort
    ///   - Most significant digit (MSD) radix sort
    /// </summary>
    internal class RadixSort<TComparable> : ISorter<TComparable> where TComparable : IComparable<TComparable>
    {
        public void Sort(IList<TComparable> list, IComparer<TComparable> comparer)
        {
            throw new NotImplementedException();
            //TODO: implement radix sort algorithm
            //http://www.codeproject.com/Articles/32382/Radix-Sorting-Implementation-with-C
            //http://en.wikipedia.org/wiki/Counting_sort
            //http://en.wikipedia.org/wiki/Radix_sort
        }
    }

    internal class RadixSortProvider : ISorterProvider
    {
        public ISorter<TComparable> GetSorter<TComparable>(string algorithmName) where TComparable : IComparable<TComparable>
        {
            return SingletonSorterProvider<RadixSort<TComparable>, TComparable>.GetSorter();
        }
    }
}
