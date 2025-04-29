using System;
using System.Collections.Generic;

namespace Merrigan0.SetsInternal {
    ////// When you have an Array that happens to be sorted and distinct, but want a Set
    ////[Untested]
    ////internal class SortedDistinctArraySet<T> : Set<T> {
    ////    private SortedArrayArray<T> sortedDistinctItems;

    ////    public override long Length { get { return sortedDistinctItems.Length; } }

    ////    public SortedDistinctArraySet(Array<T> sortedDistinctItems, Func<T, T, int> compare) {
    ////        this.sortedDistinctItems = new SortedArrayArray<T>(sortedDistinctItems, compare);
    ////    }
    ////}
}
