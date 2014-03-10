using System;
using System.Collections.Generic;
using SortingExtensions.Contracts;

namespace SortingExtensions.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckSortAlgorithm((list) => list.Sort(SortAlgorithm.Heap));
            CheckSortAlgorithm((list) => list.Sort(SortAlgorithm.Insertion));
            CheckSortAlgorithm((list) => list.Sort(SortAlgorithm.MergeBottomUp));
            CheckSortAlgorithm((list) => list.Sort(SortAlgorithm.MergeUpBottom));
            CheckSortAlgorithm((list) => list.Sort(SortAlgorithm.Quick));
            CheckSortAlgorithm((list) => list.Sort(SortAlgorithm.Selection));
            CheckSortAlgorithm((list) => list.Sort(SortAlgorithm.Shell));

            Console.ReadKey();
        }

        private static void CheckSortAlgorithm(Action<IList<int>> sortAction)
        {
            var list = new List<int>() { 10, 100, 65, 21, 8, 36, 458, 21, 0 };
            sortAction(list);
            if (list.IsSorted())
            {
                WriteToConsole("List was sorted", ConsoleColor.Green);
            } else
            {
                WriteToConsole("List was not sorted", ConsoleColor.Red);
            }
        }

        private static void WriteToConsole(string message, ConsoleColor color)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = currentColor;
        }
    }
}
