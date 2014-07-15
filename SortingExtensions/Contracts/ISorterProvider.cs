namespace SortingExtensions.Contracts
{
    using System;

    public interface ISorterProvider
    {
        ISorter<TComparable> GetSorter<TComparable>(string sorterName) where TComparable : IComparable<TComparable>;
    }
}
