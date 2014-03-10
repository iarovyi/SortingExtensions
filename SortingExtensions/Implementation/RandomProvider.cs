using System;
using System.Threading;

namespace SortingExtensions.Implementation
{
    internal static class RandomProvider
    {
        private static int _seed = Environment.TickCount;

        private static readonly ThreadLocal<Random> ThreadLocalRandom = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref _seed)));

        internal static Random GetThreadRandom()
        {
            return ThreadLocalRandom.Value;
        }
    }
}
