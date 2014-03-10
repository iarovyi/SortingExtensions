using NUnit.Framework;
using SortingExtensions.Contracts;
using SortingExtensions.Implementation;
using SortingExtensions.Implementation.Sorters;

namespace SortingExtensions.Tests.Implementation
{
    [TestFixture]
    class SingletonSorterProviderTests
    {
        [Test]
        public void SingletonProvider_Returns_The_Same_Sorter_For_One_Algorithm()
        {
            ISorter<int> sorter1 = SingletonSorterProvider<QuickSort<int>, int>.GetSorter(),
                         sorter2 = SingletonSorterProvider<QuickSort<int>, int>.GetSorter();

            Assert.That(sorter1 == sorter2);
        }

        [Test]
        public void SingletonProvider_Returns_Different_Sorters_For_Different_Algorithms()
        {
            ISorter<int> sorter1 = SingletonSorterProvider<QuickSort<int>, int>.GetSorter(),
                         sorter2 = SingletonSorterProvider<HeapSort<int>, int>.GetSorter();

            Assert.That(sorter1 != sorter2);
        } 
    }
}
