namespace SortingExtensions.Tests.Implementation.Sorters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class SorterTestBase
    {
        protected IList<int> OrderedList
        {
            get { return new List<int>(Enumerable.Range(0, 20)); }
        }

        protected IList<int> DescendingOrderedList
        {
            get { return OrderedList.Reverse().ToList(); }
        }

        protected IList<int> BigOrderedList
        {
            get { return new List<int>(Enumerable.Range(0, 10000)); }
        }

        protected IList<int> DescendingBigOrderedList
        {
            get { return OrderedList.Reverse().ToList(); }
        }

        protected IList<int> DuplicatesList
        {
            get
            {
                var random = new Random(Environment.TickCount);
                var list = new List<int>();
                for (int i = 0; i < 10; i++)
                {
                    list.AddRange(Enumerable.Repeat(random.Next(), 4));
                }

                return list;
            }
        }

        protected IEnumerable<IList<int>> DifferentLists
        {
            get
            {
                yield return OrderedList;
                yield return DescendingOrderedList;
                yield return BigOrderedList;
                yield return DescendingBigOrderedList;
                yield return DuplicatesList;
            }
        }

        protected bool IsSorted<TComparable>(IList<TComparable> list) where TComparable : IComparable<TComparable>
        {
            return list.IsSorted();
        }
    }
}
