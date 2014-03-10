using System.Collections.Generic;
using NUnit.Framework;
using SortingExtensions.Contracts;
using SortingExtensions.Implementation;
using SortingExtensions.Implementation.Sorters;

namespace SortingExtensions.Tests.Implementation
{
    [TestFixture]
    class SorterFactoryTests
    {
        public IEnumerable<TestCaseData> SorterProvidersByTypeCaseData
        {
            get
            {
                yield return new TestCaseData(SortAlgorithm.Shell).Returns(ShellSorterProvider<int>.GetSorter(SortAlgorithm.Shell));
                yield return new TestCaseData(SortAlgorithm.Quick).Returns(QuickSortProvider<int>.GetSorter(SortAlgorithm.Quick));
                yield return new TestCaseData(SortAlgorithm.Selection).Returns(SelectionSorterProvider<int>.GetSorter(SortAlgorithm.Selection));
                yield return new TestCaseData(SortAlgorithm.MergeUpBottom).Returns(MergeSorterProvider<int>.GetSorter(SortAlgorithm.MergeUpBottom));
                yield return new TestCaseData(SortAlgorithm.MergeBottomUp).Returns(MergeSorterProvider<int>.GetSorter(SortAlgorithm.MergeBottomUp));
                yield return new TestCaseData(SortAlgorithm.Insertion).Returns(InsertionSorterProvider<int>.GetSorter(SortAlgorithm.Insertion));
                yield return new TestCaseData(SortAlgorithm.Heap).Returns(HeapSorterProvider<int>.GetSorter(SortAlgorithm.Heap));
            }
        }

        public IEnumerable<TestCaseData> SorterProvidersByCaseCaseData
        {
            get
            {
                //TODO implement others
                yield return new TestCaseData(SortCase.PartiallySorted).Returns(ShellSorterProvider<int>.GetSorter(SortAlgorithm.Bubble));
            }
        }

        [Test]
        [TestCaseSource("SorterProvidersByTypeCaseData")]
        public ISorter<int> SorterFactory_Returns_Correct_Sorter_By_Algorithm(SortAlgorithm sortAlgorithm)
        {
            return SorterFactory<int>.GetSorter(sortAlgorithm);
        }

        [Test]
        [TestCaseSource("SorterProvidersByCaseCaseData")]
        public ISorter<int> SorterFactory_Returns_Correct_Sorter_ByCase(SortCase sortCase)
        {
            return SorterFactory<int>.GetSorter(sortCase);
        }
    }
}
