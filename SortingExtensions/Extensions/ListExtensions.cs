namespace SortingExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq.Expressions;
    using Contracts;
    using Extensions;
    using Implementation;
    using Implementation.Sorters.QuickSorts;

    public static class ListExtensions
    {
        #region Condition-based sorting extensions
        /*/// <summary>
        /// Sort list based on condition
        /// </summary>
        public static void Sort<TComparable>(this IList<TComparable> list, SortCase condition, IComparer<TComparable> comparer)
           where TComparable : IComparable<TComparable>
        {
            SorterFactory.GetSorter<TComparable>(condition).Sort(list, comparer);
        }

        /// <summary>
        /// Sort list based on condition
        /// </summary>
        public static void Sort<TComparable>(this IList<TComparable> list, SortCase condition, Comparison<TComparable> comparison)
            where TComparable : IComparable<TComparable>
        {
            SorterFactory.GetSorter<TComparable>(condition).Sort(list, Comparer<TComparable>.Create(comparison));
        }

        /// <summary>
        /// Sort list based on condition
        /// </summary>
        public static void Sort<TComparable>(this IList<TComparable> list, SortCase condition)
            where TComparable : IComparable<TComparable>
        {
            SorterFactory.GetSorter<TComparable>(condition).Sort(list, Comparer<TComparable>.Default);
        }*/
        #endregion

        #region Algorithms-based sorting extensions
        /// <summary>
        /// Sort algorithm
        /// </summary>
        /// <typeparam name="TComparable">type of comparable list item</typeparam>
        /// <param name="list">comparable items list</param>
        /// <param name="algorithm">sort algorithm</param>
        public static void Sort<TComparable>(this IList<TComparable> list,
                                             SortAlgorithm algorithm)
           where TComparable : IComparable<TComparable>
        {
            Contract.Requires(list != null);
            Contract.Requires(!list.IsReadOnly);

            SorterFactory.GetSorter<TComparable>(algorithm).Sort(list, Comparer<TComparable>.Default);
        }

        /// <summary>
        /// Sort list
        /// </summary>
        /// <typeparam name="TComparable">type of comparable list item</typeparam>
        /// <param name="list">list</param>
        /// <param name="algorithm">sort algorithm</param>
        /// <param name="order">sort order</param>
        /// <param name="comparer">comparison for comparing items</param>
        public static void Sort<TComparable>(this IList<TComparable> list,
                                             SortAlgorithm algorithm,
                                             SortOrder order,
                                             IComparer<TComparable> comparer = null)
           where TComparable : IComparable<TComparable>
        {
            Contract.Requires(list != null);
            Contract.Requires(!list.IsReadOnly);

            SorterFactory.GetSorter<TComparable>(algorithm).Sort(list, GetComparer(comparer, order));
        }

        /// <summary>
        /// Sort list
        /// </summary>
        /// <typeparam name="TComparable">type of comparable list item</typeparam>
        /// <param name="list">list</param>
        /// <param name="algorithm">sort algorithm</param>
        /// <param name="order">sort order</param>
        /// <param name="comparison">comparison for comparing items</param>
        public static void Sort<TComparable>(this IList<TComparable> list,
                                             SortAlgorithm algorithm,
                                             SortOrder order,
                                             Comparison<TComparable> comparison)
            where TComparable : IComparable<TComparable>
        {
            Contract.Requires(list != null);
            Contract.Requires(!list.IsReadOnly);

            SorterFactory.GetSorter<TComparable>(algorithm).Sort(list, GetComparer(comparison, order));
        }
        #endregion

        #region Sort by property or field
        /// <summary>
        /// Sort list by property lambda expression
        /// </summary>
        /// <typeparam name="T">type of list item</typeparam>
        /// <typeparam name="TComparable">type of item to compare</typeparam>
        /// <param name="list">list</param>
        /// <param name="algorithm">sort algorithm</param>
        /// <param name="property">property selection lambda expression</param>
        /// <param name="order">sort order</param>
        /// <param name="comparer">comparer to compare property</param>
        public static void Sort<T, TComparable>(this IList<T> list,
                                                SortAlgorithm algorithm,
                                                Expression<Func<T, TComparable>> property,
                                                SortOrder order,
                                                IComparer<TComparable> comparer)
            where T : IComparable<T>
            where TComparable : IComparable<TComparable>
        {
            Contract.Requires(list != null);
            Contract.Requires(!list.IsReadOnly);
            Contract.Requires(property != null);

            Func<T, TComparable> propGetter = property.Compile();
            IComparer<TComparable> propComparer = GetComparer(comparer, order);
            var comparison = new Comparison<T>((x, y) => propComparer.Compare(propGetter(x), propGetter(y)));
            SorterFactory.GetSorter<T>(algorithm).Sort(list, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Sort list by property expression
        /// </summary>
        /// <typeparam name="T">type of list item</typeparam>
        /// <typeparam name="TComparable">type of item to compare</typeparam>
        /// <param name="list">list</param>
        /// <param name="algorithm">sort algorithm</param>
        /// <param name="propertyName">property selection expression</param>
        /// <param name="order">sort order</param>
        /// <param name="comparer">comparer for comparing properties</param>
        public static void Sort<T, TComparable>(this IList<T> list,
                                                SortAlgorithm algorithm,
                                                string propertyName,
                                                SortOrder order,
                                                IComparer<TComparable> comparer = null)
            where T : IComparable<T>
            where TComparable : IComparable<TComparable>
        {
            Contract.Requires(list != null);
            Contract.Requires(!list.IsReadOnly);
            Contract.Requires(!string.IsNullOrWhiteSpace(propertyName));

            //TODO: optimaze
            Func<T, object> propGetter = x => x.GetDataMemberValue(propertyName);
            IComparer<TComparable> propComparer = GetComparer(comparer, order);
            var comparison = new Comparison<T>((x, y) => propComparer.Compare((TComparable)propGetter(x), (TComparable)propGetter(y)));
            SorterFactory.GetSorter<T>(algorithm).Sort(list, Comparer<T>.Create(comparison));
        }
        #endregion

        public static void Sort<T, TComparable>(this IList<T> list,
                                                Expression<Func<T, TComparable>> property,
                                                SortOrder order = SortOrder.Ascending)
            where TComparable : IComparable<TComparable>
        {
            Contract.Requires(list != null);
            Contract.Requires(!list.IsReadOnly);
            Contract.Requires(property != null);

            Func<T, TComparable> propertyGetter = property.Compile();
            QuickThreeWaySort<TComparable>.Sort(list, propertyGetter, GetComparer(Comparer<TComparable>.Default, order));
        }

        #region Select n-th item extensions
        /// <summary>
        /// Select n-th element of list in sort order
        /// </summary>
        /// <typeparam name="TComparable">type of comparable item</typeparam>
        /// <param name="list">comparable list</param>
        /// <param name="n">index of item in sorted comparable collection</param>
        /// <returns>n-th element of list in sort order</returns>
        public static TComparable GetBySortIndex<TComparable>(this IList<TComparable> list, int n)
            where TComparable : IComparable<TComparable>
        {
            return SelectHelper<TComparable>.Select(list, n, Comparer<TComparable>.Default);
        }

        /// <summary>
        /// Select n-th element of list in sort order
        /// </summary>
        /// <typeparam name="TComparable">type of comparable item</typeparam>
        /// <param name="list">comparable list</param>
        /// <param name="n">index of item in sorted comparable collection</param>
        /// <param name="comparer">comparer for sorting</param>
        /// <returns>n-th element of list in sort order</returns>
        public static TComparable GetBySortIndex<TComparable>(this IList<TComparable> list, int n, IComparer<TComparable> comparer)
            where TComparable : IComparable<TComparable>
        {
            return SelectHelper<TComparable>.Select(list, n, comparer);
        }

        /// <summary>
        /// Select n-th element of list in sort order
        /// </summary>
        /// <typeparam name="TComparable">type of comparable item</typeparam>
        /// <param name="list">comparable list</param>
        /// <param name="n">index of item in sorted comparable collection</param>
        /// <param name="comparison">comparison for sorting</param>
        /// <returns>n-th element of list in sort order</returns>
        public static TComparable GetBySortIndex<TComparable>(this IList<TComparable> list, int n, Comparison<TComparable> comparison)
            where TComparable : IComparable<TComparable>
        {
            return SelectHelper<TComparable>.Select(list, n, Comparer<TComparable>.Create(comparison));
        }
        #endregion

        /// <summary>
        /// Check whether list is sorted
        /// </summary>
        /// <typeparam name="TComparable">type of comparable item</typeparam>
        /// <param name="list">comparable collection</param>
        /// <returns>is sorted</returns>
        [Pure]
        public static bool IsSorted<TComparable>(this IList<TComparable> list)
            where TComparable : IComparable<TComparable>
        {
            return list.IsSorted(0, list.Count - 1, Comparer<TComparable>.Default);
        }

        /// <summary>
        /// Check whether list is sorted by key
        /// </summary>
        /// <typeparam name="T">type of item</typeparam>
        /// <typeparam name="TComparable">type of comparable key</typeparam>
        /// <param name="list">collection</param>
        /// <param name="propertyGetter">key selector</param>
        /// <param name="order">sort order</param>
        /// <returns>is sorted</returns>
        [Pure]
        public static bool IsSorted<T, TComparable>(this IList<T> list,
                                                    Func<T,TComparable> propertyGetter,
                                                    SortOrder order = SortOrder.Ascending)
            where TComparable : IComparable<TComparable>
        {
            return list.IsSorted(0, list.Count - 1, propertyGetter, Comparer<TComparable>.Default, order);
        }

        #region Not Public Extensions
        /// <summary>
        /// Shuffle list with "Fisher-Yates Shuffle" algorithm.
        /// http://en.wikipedia.org/wiki/Fisher-Yates_shuffle
        /// </summary>
        internal static void Shuffle<TComparable>(this IList<TComparable> list)
            where TComparable : IComparable<TComparable>
        {
            Contract.Requires(list != null);
            Contract.Requires(!list.IsReadOnly);

            int length = list.Count;
            Random random = RandomProvider.GetThreadRandom();

            for (int i = length; i > 1; i--)
            {
                int randomIndex = random.Next(i);
                list.Exchange(i - 1, randomIndex);
            }
        }

        [Pure]
        internal static bool IsSorted<TComparable>(this IList<TComparable> list,
                                                   int startIndex,
                                                   int endIndex,
                                                   IComparer<TComparable> comparer,
                                                   SortOrder order = SortOrder.Ascending)
            where TComparable : IComparable<TComparable>
        {
            return list.IsSorted(startIndex, endIndex, item => item, comparer, order);
        }

       [Pure]
        internal static bool IsSorted<T, TComparable>(this IList<T> list,
                                                   int startIndex,
                                                   int endIndex,
                                                   Func<T, TComparable> itemGetter,
                                                   IComparer<TComparable> comparer,
                                                   SortOrder order = SortOrder.Ascending)
            where TComparable : IComparable<TComparable>
        {
            Contract.Requires(list != null);
            Contract.Requires(itemGetter != null);
            Contract.Requires(startIndex >= 0);
            Contract.Requires(startIndex <= list.Count - 1);
            Contract.Requires(endIndex >= 0);
            Contract.Requires(endIndex <= list.Count - 1);
            Contract.Requires(startIndex <= endIndex);
            Contract.Requires(comparer != null);

            bool ascendingOrder = order == SortOrder.Ascending;
            for (int i = startIndex; i < endIndex; i++)
            {
                int result = comparer.Compare(itemGetter(list[i]), itemGetter(list[i + 1]));
                if (ascendingOrder ? result > 0 : result < 0)
                {
                    return false;
                }
            }

            return true;
        }

        internal static void Exchange<T>(this IList<T> list, int index1, int index2)
        {
            Contract.Requires(list != null);
            Contract.Requires(!list.IsReadOnly);

            T temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }

        internal static void ExchangeByPosition<T>(this IList<T> list, int position1, int position2)
        {
            Contract.Requires(list != null);
            Contract.Requires(!list.IsReadOnly);

            list.Exchange(position1 - 1, position2 - 1);
        }


        #endregion

        #region Private Methods 
        private static IComparer<T> GetComparer<T>(IComparer<T> comparer = null, SortOrder order = SortOrder.Ascending)
        {
            var selectedComparer = comparer ?? Comparer<T>.Default;
            return order == SortOrder.Ascending ? selectedComparer : new ReversedComparer<T>(selectedComparer);
        }

        private static IComparer<T> GetComparer<T>(Comparison<T> comparison = null, SortOrder order = SortOrder.Ascending)
        {
            IComparer<T> selectedComparer = comparison != null ? Comparer<T>.Create(comparison) : Comparer<T>.Default;
            return order == SortOrder.Ascending ? selectedComparer : new ReversedComparer<T>(selectedComparer);
        }
        #endregion
    }
}
