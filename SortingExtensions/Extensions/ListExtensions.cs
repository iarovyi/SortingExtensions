using System;
using System.Collections.Generic;
using SortingExtensions.Contracts;
using SortingExtensions.Extensions;
using SortingExtensions.Implementation;

namespace SortingExtensions
{
    
    public static class ListExtensions
    {
        #region Condition-based sorting extensions
        /// <summary>
        /// Sort list based on condition
        /// </summary>
        public static void Sort<TComparable>(this IList<TComparable> list, SortCase condition, IComparer<TComparable> comparer)
           where TComparable : IComparable<TComparable>
        {
            SorterFactory<TComparable>.GetSorter(condition).Sort(list, comparer);
        }

        /// <summary>
        /// Sort list based on condition
        /// </summary>
        public static void Sort<TComparable>(this IList<TComparable> list, SortCase condition, Comparison<TComparable> comparison)
            where TComparable : IComparable<TComparable>
        {
            SorterFactory<TComparable>.GetSorter(condition).Sort(list, Comparer<TComparable>.Create(comparison));
        }

        /// <summary>
        /// Sort list based on condition
        /// </summary>
        public static void Sort<TComparable>(this IList<TComparable> list, SortCase condition)
            where TComparable : IComparable<TComparable>
        {
            SorterFactory<TComparable>.GetSorter(condition).Sort(list, Comparer<TComparable>.Default);
        }
        #endregion

        #region Algorithms-based sorting extensions
        /// <summary>
        /// Sort list based on algorithm
        /// </summary>
        public static void Sort<TComparable>(this IList<TComparable> list, SortAlgorithm algorithm, IComparer<TComparable> comparer)
           where TComparable : IComparable<TComparable>
        {
            SorterFactory<TComparable>.GetSorter(algorithm).Sort(list, comparer);
        }

        /// <summary>
        /// Sort list based on algorithm
        /// </summary>
        public static void Sort<TComparable>(this IList<TComparable> list, SortAlgorithm algorithm, Comparison<TComparable> comparison)
            where TComparable : IComparable<TComparable>
        {
            SorterFactory<TComparable>.GetSorter(algorithm).Sort(list, Comparer<TComparable>.Create(comparison));
        }

        /// <summary>
        /// Sort list based on algorithm
        /// </summary>
        public static void Sort<TComparable>(this IList<TComparable> list, SortAlgorithm algorithm)
            where TComparable : IComparable<TComparable>
        {
            SorterFactory<TComparable>.GetSorter(algorithm).Sort(list, Comparer<TComparable>.Default);
        }
        #endregion

        /// <summary>
        /// Check whether list is sorted
        /// </summary>
        public static bool IsSorted<TComparable>(this IList<TComparable> list)
            where TComparable : IComparable<TComparable>
        {
            return list.IsSorted(Comparer<TComparable>.Default);
        }

        #region Internal extensions
        /// <summary>
        /// Shuffle list with "Fisher-Yates Shuffle" algorithm.
        /// http://en.wikipedia.org/wiki/Fisher-Yates_shuffle
        /// </summary>
        internal static void Shuffle<TComparable>(this IList<TComparable> list)
            where TComparable : IComparable<TComparable>
        {
            int length = list.Count;
            Random random = RandomProvider.GetThreadRandom();

            //
            for (int i = length; i > 1; i--)
            {
                int randomIndex = random.Next(i);
                list.Exchange(i - 1, randomIndex);
            }
        }

        internal static bool IsSorted<TComparable>(this IList<TComparable> list, IComparer<TComparable> comparer, bool ascendingOrder = true)
            where TComparable : IComparable<TComparable>
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                int result = comparer.Compare(list[i], list[i + 1]);
                if (ascendingOrder ? result > 0 : result < 0)
                {
                    return false;
                }
            }

            return true;
        }
        #endregion
    }
}
