using System;
using System.Collections.Generic;
using SortingExtensions.Contracts;

namespace SortingExtensions.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Sort by item property
            CheckIsArraySortedByProperty(man => man.Education.Level, manArray =>
            {
                // ReSharper disable once ConvertToLambdaExpression
                manArray.Sort(SortAlgorithm.Quick, "Education.Level", SortOrder.Ascending, Comparer<int>.Default);
            });

            CheckIsArraySortedByProperty(man => man.Education.Level, manArray =>
            {
                // ReSharper disable once ConvertToLambdaExpression
                manArray.Sort(SortAlgorithm.Quick, m => m.Education.Level, SortOrder.Ascending, Comparer<int>.Default);
            });


            // Different sorting algorithms
            CheckSortAlgorithm((list) => list.Sort(SortAlgorithm.Bubble));
            CheckSortAlgorithm((list) => list.Sort(SortAlgorithm.MergeUpBottomSortForPartiallySorted));
            CheckSortAlgorithm((list) => list.Sort(SortAlgorithm.MergeUpBottomSortWithCutoff));
            CheckSortAlgorithm((list) => list.Sort(SortAlgorithm.QuickWithMedianOfThree));
            CheckSortAlgorithm((list) => list.Sort(SortAlgorithm.QuickWithCutoff));
            CheckSortAlgorithm((list) => list.Sort(SortAlgorithm.Quick));
            CheckSortAlgorithm((list) => list.Sort(SortAlgorithm.QuickThreeWay));
            CheckSortAlgorithm((list) => list.Sort(SortAlgorithm.Heap));
            CheckSortAlgorithm((list) => list.Sort(SortAlgorithm.Insertion));
            CheckSortAlgorithm((list) => list.Sort(SortAlgorithm.MergeBottomUp));
            CheckSortAlgorithm((list) => list.Sort(SortAlgorithm.MergeUpBottom));
            CheckSortAlgorithm((list) => list.Sort(SortAlgorithm.Quick));
            CheckSortAlgorithm((list) => list.Sort(SortAlgorithm.Selection));
            CheckSortAlgorithm((list) => list.Sort(SortAlgorithm.Shell));

            Console.ReadKey();
        }

        #region Private methods
        private static void CheckSortAlgorithm(Action<IList<int>> sortAction)
        {
            var list = new List<int>() { 10, 100, 65, 21, 8, 36, 458, 21, 0 };
            sortAction(list);
            if (list.IsSorted()) {
                WriteToConsole("List was sorted", ConsoleColor.Green);
            } else {
                WriteToConsole("List was not sorted", ConsoleColor.Red);
            }
        }

        private static void CheckIsArraySortedByProperty(Func<Man, int> sortPropertyGetter,
                                                         Action<Man[]> sortAction)
        {
            var list = new[]
                {
                    new Man() {Age = 100,  Education = new Education() {Level = 8}},
                    new Man() {Age = 7,    Education = new Education() {Level = 2}},
                    new Man() {Age = 12,   Education = new Education() {Level = 6}},
                    new Man() {Age = 1000, Education = new Education() {Level = 5}},
                    new Man() {Age = 25,   Education = new Education() {Level = 11}},
                    new Man() {Age = 100,  Education = new Education() {Level = 8}}
                };

            sortAction(list);
            if (list.IsSorted(sortPropertyGetter))
            {
                WriteToConsole("Array was sorted", ConsoleColor.Green);
            }
            else
            {
                WriteToConsole("Array was not sorted", ConsoleColor.Red);
            }
        }

        private static void WriteToConsole(string message, ConsoleColor color)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = currentColor;
        }
        #endregion
    }

    #region Sample Classes
    class Man : IComparable<Man>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Education Education { get; set; }

        public int CompareTo(Man other)
        {
            return String.Compare(Name, other.Name, StringComparison.Ordinal);
        }
    }

    class Education
    {
        public int Level;
    }
    #endregion
}
