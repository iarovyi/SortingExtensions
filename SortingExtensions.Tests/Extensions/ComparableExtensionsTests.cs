﻿namespace SortingExtensions.Tests.Extensions
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using SortingExtensions.Extensions;

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
            Assert.That(4.IsLessThan(5, Comparer<int>.Default));
        }
    }
}
