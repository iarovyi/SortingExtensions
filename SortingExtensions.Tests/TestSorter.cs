namespace SortingExtensions.Tests
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using SortingExtensions.Implementation;

    public class TestSorter<TComparable> : ISorter<TComparable> where TComparable : IComparable<TComparable>
    {
        public void Sort(IList<TComparable> list, IComparer<TComparable> comparer)
        {
            SorterFactory.GetSorter<TComparable>(SortAlgorithm.Quick).Sort(list, comparer);
        }
    }
}
