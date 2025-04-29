using System;
using System.Collections.Generic;

namespace Merrigan0.SetsInternal {
    ////// When you have an already sorted, already distinct bytes but want a Set
    ////[Untested]
    ////internal class SortedDistinctBlockSet<T> : Set<T> {
    ////    private T[] sortedDistinctItems;

    ////    public override long Length { get { return sortedDistinctItems.LongLength; } }

    ////    public SortedDistinctBlockSet(T[] sortedDistinctItems, Func<T, T, int> compare) :
    ////        base(compare) {
    ////        this.sortedDistinctItems = sortedDistinctItems;
    ////    }

    ////    protected override T GetItem(long i) { return sortedDistinctItems[i]; }
    ////}
}
