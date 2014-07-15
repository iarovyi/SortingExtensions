namespace SortingExtensions.Tests
{
    using System;
    using Contracts;

    class TestSortProvider : ISorterProvider
    {
        public ISorter<TComparable> GetSorter<TComparable>(string sorterName) where TComparable : IComparable<TComparable>
        {
            return new TestSorter<TComparable>();
        }
    }
}
