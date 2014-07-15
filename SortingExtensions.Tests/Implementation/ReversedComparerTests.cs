namespace SortingExtensions.Tests.Implementation
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using SortingExtensions.Implementation;

    [TestFixture]
    class ReversedComparerTests
    {
        [Test]
        public void ReversedComparer_Compares_Reversed()
        {
            var reversedComparer = new ReversedComparer<int>(Comparer<int>.Default);

            Assert.That(reversedComparer.Compare(100, 1) < 0);
        }
    }
}
