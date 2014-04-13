using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using SortingExtensions.Extensions;

namespace SortingExtensions.Implementation.Sorters.QuickSorts
{
    /// <summary>
    /// Dijkstra proved that simple Quick sort can become quadratic on duplicate keys.
    /// This problem can be solved with merge sort or 3-way quick sort.
    /// 
    /// Performance: NlgN compares when all distinct, linear when only a constant number of distinct keys
    /// http://www.sorting-algorithms.com/quick-sort-3-way
    /// </summary>
    internal class QuickThreeWaySort<TComparable> : BaseQuickSort<TComparable> where TComparable : IComparable<TComparable>
    {
        protected override void Sort(IList<TComparable> list, int lo, int hi, IComparer<TComparable> comparer)
        {
            Contract.Requires(list != null);
            Contract.Requires(comparer != null);
            Contract.Requires(lo >= 0 && lo <= list.Count);
            Contract.Requires(hi < list.Count);
            Contract.Ensures((lo <= hi && list.IsSorted(lo, hi, comparer)) || lo > hi);
            Contract.EndContractBlock();

            if (hi <= lo) return;

            int lt = lo,
                gt = hi;
            TComparable pivotValue = list[lo];
            int i = lo;
            while (i <= gt)
            {
                int cmp = comparer.Compare(list[i], pivotValue);
                if      (cmp < 0) list.Exchange(lt++, i++);
                else if (cmp > 0) list.Exchange(i, gt--);
                else              i++;
            }

            Sort(list, lo, lt - 1, comparer);
            Sort(list, gt + 1, hi, comparer);
        }

        #region Sort with property getter
        public static void Sort<T>(IList<T> list, Func<T, TComparable> propertyGetter, IComparer<TComparable> comparer)
        {
            Sort(list, propertyGetter, 0, list.Count - 1, comparer);
        }

        private static void Sort<T>(IList<T> list, Func<T, TComparable> propertyGetter, int lo, int hi, IComparer<TComparable> comparer)
        {
            Contract.Requires(list != null);
            Contract.Requires(comparer != null);
            Contract.Requires(lo >= 0 && lo <= list.Count);
            Contract.Requires(hi < list.Count);
            Contract.EndContractBlock();

            if (hi <= lo) return;

            int lt = lo,
                gt = hi;
            TComparable pivotValue = propertyGetter(list[lo]);
            int i = lo;
            while (i <= gt)
            {
                int cmp = comparer.Compare(propertyGetter(list[i]), pivotValue);
                if (cmp < 0)      list.Exchange(lt++, i++);
                else if (cmp > 0) list.Exchange(i, gt--);
                else              i++;
            }

            Sort(list, propertyGetter, lo, lt - 1, comparer);
            Sort(list, propertyGetter, gt + 1, hi, comparer);
        }
        #endregion
    }
}
