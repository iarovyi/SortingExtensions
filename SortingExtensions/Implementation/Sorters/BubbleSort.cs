namespace SortingExtensions.Implementation.Sorters
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Contracts;
    using Extensions;

    internal class BubbleSort<TComparable> : ISorter<TComparable> where TComparable : IComparable<TComparable>
    {
        public void Sort(IList<TComparable> list, IComparer<TComparable> comparer)
        {
            Contract.Requires(list != null);
            Contract.Requires(comparer != null);

            Sort(list, 0, list.Count - 1, comparer);
        }

        private void Sort(IList<TComparable> list, int lo, int hi, IComparer<TComparable> comparer)
        {
            Contract.Requires(list != null);
            Contract.Requires(comparer != null);
            Contract.Requires(lo >= 0);
            Contract.Requires(hi < list.Count);
            Contract.Requires(lo <= hi);
            Contract.Ensures(list.IsSorted(lo, hi, comparer));

            bool madeChanges;

            do
            {
                madeChanges = false;
                hi--;
                for (int i = lo; i <= hi; i++)
                {
                    if (list[i].IsBiggerThan(list[i + 1], comparer))
                    {
                        list.Exchange(i, i + 1);
                        madeChanges = true;
                    }
                }

            } while (madeChanges);
        }
    }

    internal class BubbleSortProvider : ISorterProvider
    {
        public ISorter<TComparable> GetSorter<TComparable>(string algorithmName) where TComparable : IComparable<TComparable>
        {
            return SingletonSorterProvider<BubbleSort<TComparable>, TComparable>.GetSorter();
        }
    }
}
