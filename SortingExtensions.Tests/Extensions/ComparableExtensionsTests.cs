using System.Collections.Generic;
using NUnit.Framework;
using SortingExtensions.Extensions;

namespace SortingExtensions.Tests.Extensions
{
    [TestFixture]
    class ComparableExtensionsTests
    {
        [Test]
        public void IsBiggerThan_Works_Correct()
        {
            Assert.That(5.IsBiggerThan(4, Comparer<int>.Default));
        }

        [Test]
        public void IsLessThan_Works_Correct()
        {
            Assert.That(4.IsLessThan(4, Comparer<int>.Default));
        }
    }
}
