namespace SortingExtensions.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    [ContractClass(typeof(ISorterContract<>))]
    public interface ISorter<TComparable> where TComparable : IComparable<TComparable>
    {
        void Sort(IList<TComparable> list, IComparer<TComparable> comparer);
    }

    [ContractClassFor(typeof(ISorter<>))]
    public abstract class ISorterContract<TComparable> : ISorter<TComparable> where TComparable : IComparable<TComparable>
    {
        public void Sort(IList<TComparable> list, IComparer<TComparable> comparer)
        {
            if (list == null)
                throw new ArgumentNullException("list");
            if (comparer == null)
                throw new ArgumentNullException("comparer");
            Contract.Requires(!list.IsReadOnly);
            Contract.Ensures(list.IsSorted(0, list.Count - 1, comparer));
            Contract.EndContractBlock();
        }
    }
}
