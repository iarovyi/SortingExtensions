using System;

namespace SortingExtensions.Contracts
{
    //TODO: implement selection of sort algorithm by sort case                       http://www.codeproject.com/Articles/6792/Masks-and-flags-using-bit-fields-in-NET
    //http://en.wikipedia.org/wiki/Greedy_algorithm
    [Flags]
    public enum SortCase
    {
        PartiallySorted = 1 << 0,
        Small           = 1 << 1,
        Huge            = 1 << 2,
        Stable          = 1 << 3
    }
}
