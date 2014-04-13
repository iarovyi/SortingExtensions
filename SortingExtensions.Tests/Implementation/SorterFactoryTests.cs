using System.Collections.Generic;
using NUnit.Framework;
using SortingExtensions.Contracts;
using SortingExtensions.Implementation;
using SortingExtensions.Implementation.Sorters;
using SortingExtensions.Implementation.Sorters.MergeSorts;
using SortingExtensions.Implementation.Sorters.QuickSorts;

namespace SortingExtensions.Tests.Implementation
{
    [TestFixture]
    class SorterFactoryTests
    {
        public IEnumerable<TestCaseData> SorterProvidersByTypeCaseData
        {
            get
            {
                yield return new TestCaseData(SortAlgorithm.Shell).Returns(new ShellSorterProvider().GetSorter<int>(SortAlgorithm.Shell.ToString()));
                yield return new TestCaseData(SortAlgorithm.Quick).Returns(new QuickSortProvider().GetSorter<int>(SortAlgorithm.Quick.ToString()));
                yield return new TestCaseData(SortAlgorithm.Selection).Returns(new SelectionSorterProvider().GetSorter<int>(SortAlgorithm.Selection.ToString()));
                yield return new TestCaseData(SortAlgorithm.MergeUpBottom).Returns(new MergeSorterProvider().GetSorter<int>(SortAlgorithm.MergeUpBottom.ToString()));
                yield return new TestCaseData(SortAlgorithm.MergeBottomUp).Returns(new MergeSorterProvider().GetSorter<int>(SortAlgorithm.MergeBottomUp.ToString()));
                yield return new TestCaseData(SortAlgorithm.Insertion).Returns(new InsertionSorterProvider().GetSorter<int>(SortAlgorithm.Insertion.ToString()));
                yield return new TestCaseData(SortAlgorithm.Heap).Returns(new HeapSorterProvider().GetSorter<int>(SortAlgorithm.Heap.ToString()));
            }
        }

        public IEnumerable<TestCaseData> SorterProvidersByCaseCaseData
        {
            get
            {
                //TODO implement others
                yield return new TestCaseData(SortCase.PartiallySorted).Returns(new ShellSorterProvider().GetSorter<int>(SortAlgorithm.Bubble.ToString()));
            }
        }

        [Test]
        [TestCaseSource("SorterProvidersByTypeCaseData")]
        public ISorter<int> SorterFactory_Returns_Correct_Sorter_By_Algorithm(SortAlgorithm sortAlgorithm)
        {
            return SorterFactory.GetSorter<int>(sortAlgorithm);
        }

        [Test]
        [TestCaseSource("SorterProvidersByCaseCaseData")]
        public ISorter<int> SorterFactory_Returns_Correct_Sorter_ByCase(SortCase sortCase)
        {
            return SorterFactory.GetSorter<int>(sortCase);
        }
    }
}
