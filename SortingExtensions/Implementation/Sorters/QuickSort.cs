using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SortingExtensions.Contracts;
using SortingExtensions.Extensions;

namespace SortingExtensions.Implementation.Sorters
{
    /*
     * It can overflow function call stack in Java and crash program.
     * 
     * avarage: 2NlnN (1.39NlgN) compares and (1/3)*NlnN exchanges
     * 39% more compares than mergesort but faster than merge sort in practive because of less data movement.
     * Quick sort is not stable
     * 
         * NlogN - middle
         * 1) Pick an element, called a pivot, from the list.
         * 2) Reorder the list so that all elements with values less than the pivot come before the pivot, while all elements with values
         *    greater than the pivot come after it (equal values can go either way). After this partitioning, the pivot is in its final position. 
         *    This is called the partition operation.
         * 3) Recursively apply the above steps to the sub-list of elements with smaller values and separately the sub-list of elements
         *    with greater values.
     *    
     * Quick sort can become quadratic on duplicate keys.(Merge sort is better in this situation)
     * 
     * 
     * 1) Shuffle the array
     * 2) Partition so that, for some j
     *    - entry a[j] is in place
     *    - no larger entry to the left of j
     *    -no smaller entry to ther right of j
     *  3) Sort each piece recursively
     * 
     * 
     * Partitioning item:
     *  - small arrays: middle entry
     *  - medium arrays: median of 3
     *  - large arrays: Tukey's ninther
         */

    internal class QuickSort<TComparable> : ISorter<TComparable> where TComparable : IComparable<TComparable>
    {
        /*private static int _seed = Environment.TickCount;
        private readonly ThreadLocal<Random> _randomThreadLocal = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref _seed)));*/
        private static readonly int CUTOFF = 10;

        /*
         * Select k-nth element
         * 0.5N^2 compares in worst case.
         * takes linear time on avarage
         */
        internal TComparable Select(IList<TComparable> list, int k)
        {
            list.Shuffle();
            int lo = 0, hi = list.Count - 1;
            while (hi > lo)
            {
                int j = Partition(list, lo, hi);
                if      (j < k) lo = j + 1;
                else if (j < k) hi = j - 1;
                else            return list[k];
            }

            return list[k];
        }

        public void Sort(IList<TComparable> list, IComparer<TComparable> comparer)
        {
            /*Contract.Requires(keys != null);
            Contract.Requires(values != null);
            Contract.Requires(lo >= 0);
            Contract.Requires(hi > lo);
            Contract.Requires(hi < keys.Length);
            Contract.Ensures(Contract.Result<int>() >= lo && Contract.Result<int>() <= hi);*/


            //shuffle needed for performance guarantee because in worst case number of compares is quadratic (0.5N^2)
            //Randomized quicksort with 3-way paritioning reduces running time from liearithimic to linear in broad class of applications.
            list.Shuffle();

            //QuickSorting(list, 0, list.Count - 1);
            ThreeWayQuickSort(list, 0, list.Count - 1, comparer);

            //TODO Contract.Assert everywhere
            //TODO 3-way sort into separated sort
            //TODO put internal everywhere
            //TODO add many exceptions like in library
            //TODO add localization
            //TODO add comparer every where
            Contract.Assert(list.IsSorted(comparer), "Array was not sorted");
        }

        /*
         * Dijkstra
         * Simple Quick sort can become quadratic on duplicate keys.
         * To solse this problem
         * 1) use merge sort
         * 2) use 3-way quick sort 
         * 
         * NlgN compares when all distinct
         * linear when only a constant number of distinct keys
         */
        internal void ThreeWayQuickSort(IList<TComparable> list, int lo, int hi, IComparer<TComparable> comparer)
        {
            if (hi <= lo) return;

            int lt = lo,
                gt = hi;
            TComparable v = list[lo];
            int i = lo;
            while (i <= gt)
            {
                int cmp = list[i].CompareTo(v);
                if      (cmp < 0) list.Exchange(lt++, i++);
                else if (cmp > 0) list.Exchange(i, gt--);
                else              i++;
            }

            ThreeWayQuickSort(list, lo, lt - 1, comparer);
            ThreeWayQuickSort(list, gt + 1, hi, comparer);
        }

        private static void QuickSorting(IList<TComparable> list, int lo, int hi)
        {
            #region Improvement
            /*
             * Quick sort has too much overhead for small arrays
             */
            if (hi <= lo) return;
            /*if (hi <= lo + CUTOFF - 1)
            {
                new InsertionSort().Sort(list, lo, hi);
                return;
            }*/
            #endregion

            #region Improvement
            /*
             * Best chose if pivot item = median
             * Slightly decrese number of compares, increases number of exchanges
             * Improve by 10%    http://stackoverflow.com/questions/7559608/median-of-three-values-strategy
             */
            /*int m = medianOf3(list, lo, lo + (hi - lo)/2, hi);
            list.Exchange(lo, m);*/
            #endregion

            int j = Partition(list, lo, hi);

            QuickSorting(list, lo, j - 1);
            QuickSorting(list, j + 1, hi);
        }

        /*
         * select leftmost, middle and rightmost element
            order them to the left partition, pivot and right partition. Use the pivot in the same fashion as regular quicksort.
         */
        private static int medianOf3(IList<TComparable> list, int left, int center, int right)
        {
            //int center = (left + right) / 2;
            // order left & center
            if (list[left].IsBiggerThan(list[center]))
                list.Exchange(left, center);
            // order left & right
            if (list[left].IsBiggerThan(list[right]))
                list.Exchange(left, right);
            // order center & right
            if (list[center].IsBiggerThan(list[right]))
                list.Exchange(center, right);

            list.Exchange(center, right - 1); // put pivot on right
            //return list[right - 1]; // return median value
            return right - 1;
        }
        

        // partition the subarray a[lo..hi] so that a[lo..j-1] <= a[j] <= a[j+1..hi]
        // and return the index j.

        /*
         * 1) select first element(lo) as pivot number
         * 2) go from left(i) and go from right(j) until i meet j.
         *       2.1  go from left to right(i) until we find element bigger then pivot
         *       2.2  go from right to left(j) until we find element smaller then pivot
         *       2.3  exchange them
         * 3) exchange first element and a[j]
         */
        private static int Partition(IList<TComparable> list, int lo, int hi)
        {
            int i = lo;
            int j = hi + 1;
            TComparable v = list[lo];
            while (true)
            {

                // find item on lo to swap
                while (list[++i].IsLessThan(v))
                    if (i == hi) break;

                // find item on hi to swap
                while (v.IsLessThan(list[--j]))
                    if (j == lo) break;      // redundant since a[lo] acts as sentinel

                // check if pointers cross
                if (i >= j) break;

                list.Exchange(i, j);
            }

            // put partitioning item v at a[j]
            list.Exchange(lo, j);

            // now, a[lo .. j-1] <= a[j] <= a[j+1 .. hi]
            return j;
        }
    }

    internal static class QuickSortProvider<TComparable> where TComparable : IComparable<TComparable>
    {
        internal static ISorter<TComparable> GetSorter(SortAlgorithm algorithm)
        {
            return SingletonSorterProvider<QuickSort<TComparable>, TComparable>.GetSorter();
        }
    }
}
