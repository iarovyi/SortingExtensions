using System;
using System.Threading;
using SortingExtensions.Contracts;

namespace SortingExtensions.Implementation
{
    internal static class SingletonSorterProvider<TSorter, TComparable>
        where TSorter : class, ISorter<TComparable>, new()
        where TComparable : IComparable<TComparable>
    {
        private static TSorter _sorterInstance;

        internal static ISorter<TComparable> GetSorter()
        {
            if (_sorterInstance == null)
            {
                Interlocked.CompareExchange<TSorter>(ref _sorterInstance, new TSorter(), null);
            }

            return _sorterInstance;
        }
    }
}
