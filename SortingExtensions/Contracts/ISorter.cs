using System;
using System.Collections.Generic;

namespace SortingExtensions.Contracts
{
    public interface ISorter<TComparable> where TComparable : IComparable<TComparable>
    {
        void Sort(IList<TComparable> list, IComparer<TComparable> comparer);
    }
}
