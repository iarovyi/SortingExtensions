namespace SortingExtensions.Implementation
{
    using System.Collections.Generic;

    class ReversedComparer<T> : IComparer<T>
    {
        private readonly IComparer<T> _comparer;

        public ReversedComparer(IComparer<T> comparer)
        {
            _comparer = comparer;
        }

        public int Compare(T x, T y)
        {
            return _comparer.Compare(y, x);
        }
    }
}
