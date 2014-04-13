using System.Collections.Generic;
using NUnit.Framework;
using SortingExtensions.Implementation;

namespace SortingExtensions.Tests.Implementation
{
    [TestFixture]
    class SelectHelperTest
    {
        [Test]
        public void SelectHelper_Selects_Correct_Item_By_Index()
        {
            var numbers = new[] {7, 6, 5, 4, 3, 2, 1};

            int thirdItem = SelectHelper<int>.Select(numbers, 2, Comparer<int>.Default);

            Assert.That(thirdItem == 3);
        }
    }
}
