using System.Collections.Generic;
using NUnit.Framework;

namespace SortingExtensions.Tests.Extensions
{
    //TODO: test all extension methods
    [TestFixture]
    class ListExtensionsTests
    {
        [Test]
        public void IsSorted_Detects_SortedList()
        {
            var sortedList = new List<int>() {1, 2, 3};

            Assert.That(sortedList.IsSorted());
        }

        [Test]
        public void IsSorted_Detects_NotSortedList()
        {
            var notSortedList = new List<int>() { 1, 3, 2 };

            Assert.That(notSortedList.IsSorted() == false);
        }
    }
}
