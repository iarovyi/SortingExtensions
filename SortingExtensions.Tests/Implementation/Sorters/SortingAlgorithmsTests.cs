﻿using System.Collections.Generic;
using NUnit.Framework;
using SortingExtensions.Contracts;

namespace SortingExtensions.Tests.Implementation.Sorters
{
    [TestFixture]
    class SortingAlgorithmsTests : SorterTestBase
    {
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