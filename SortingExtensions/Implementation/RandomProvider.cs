namespace SortingExtensions.Implementation
{
    using System;
    using System.Threading;

    internal static class RandomProvider
    {
        private static int _seed = Environment.TickCount;

        //Alternative: private static readonly ThreadLocal<Random> ThreadLocalRandom = new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode()));
        private static readonly ThreadLocal<Random> ThreadLocalRandom = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref _seed)));

        internal static Random GetThreadRandom()
        {
            return ThreadLocalRandom.Value;
        }
    }
}
