using System;

namespace SortingExtensions.Contracts
{
    public interface ISorterProvider
    {
        ISorter<TComparable> GetSorter<TComparable>(string sorterName) where TComparable : IComparable<TComparable>;
    }
}
