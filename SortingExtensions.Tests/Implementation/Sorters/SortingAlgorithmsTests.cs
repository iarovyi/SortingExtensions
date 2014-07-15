namespace SortingExtensions.Tests.Implementation.Sorters
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using Contracts;
    using SortingExtensions.Implementation;

    [TestFixture]
    class SortingAlgorithmsTests : SorterTestBase
    {
        [Test]
        [TestCaseSource("DifferentLists")]
        public void All_Registered_Sorters_Sorts_Correctly(IList<int> list)
        {
            foreach (string algorithmName in Enum.GetNames(typeof(SortAlgorithm)))
            {
                ISorter<int> sorter = SorterFactory.GetSorter<int>(algorithmName);
                var clonedList = new List<int>(list);
                sorter.Sort(clonedList, Comparer<int>.Default);

                Assert.That(IsSorted(clonedList));
            }
        }

        [Test]
        [TestCaseSource("DifferentLists")]
        public void HeapSort_Sorts_List(IList<int> list)
        {
            list.Sort(SortAlgorithm.Heap);

            Assert.That(IsSorted(list));
        }

        [Test]
        [TestCaseSource("DifferentLists")]
        public void Quick_Sorts_List(IList<int> list)
        {
            list.Sort(SortAlgorithm.Quick);

            Assert.That(IsSorted(list));
        }

        [Test]
        [TestCaseSource("DifferentLists")]
        public void Insertion_Sorts_List(IList<int> list)
        {
            list.Sort(SortAlgorithm.Insertion);

            Assert.That(IsSorted(list));
        }

        [Test]
        [TestCaseSource("DifferentLists")]
        public void MergeBottomUp_Sorts_List(IList<int> list)
        {
            list.Sort(SortAlgorithm.MergeBottomUp);

            Assert.That(IsSorted(list));
        }

        [Test]
        [TestCaseSource("DifferentLists")]
        public void MergeUpBottom_Sorts_List(IList<int> list)
        {
            list.Sort(SortAlgorithm.MergeUpBottom);

            Assert.That(IsSorted(list));
        }

        [Test]
        [TestCaseSource("DifferentLists")]
        public void Selection_Sorts_List(IList<int> list)
        {
            list.Sort(SortAlgorithm.Selection);

            Assert.That(IsSorted(list));
        }

        [Test]
        [TestCaseSource("DifferentLists")]
        public void Shell_Sorts_List(IList<int> list)
        {
            list.Sort(SortAlgorithm.Shell);

            Assert.That(IsSorted(list));
        }
    }
}
