using System;
using SortingExtensions.Contracts;

namespace SortingExtensions.Tests
{
    class TestSortProvider : ISorterProvider
    {
        public ISorter<TComparable> GetSorter<TComparable>(string sorterName) where TComparable : IComparable<TComparable>
        {
            return new TestSorter<TComparable>();
        }
    }
}
