using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.SetsInternal {
    ////// When you have an already sorted, already distinct IList but want a Set
    ////[Untested]
    ////internal class SortedDistinctListSet<T> : Set<T> {
    ////    private IList<T> sortedDistinctItems;

    ////    public override long Length { get { return sortedDistinctItems.Count; } }

    ////    public SortedDistinctListSet(IList<T> sortedDistinctItems, Func<T, T, int> compare) :
    ////        base(compare) {
    ////        this.sortedDistinctItems = sortedDistinctItems;
    ////    }

    ////    protected override T GetItem(long i) {
    ////        if (i > int.MaxValue) {
    ////            throw new IndexOutOfRangeException();
    ////        }
    ////        return sortedDistinctItems[(int)i];
    ////    }
    ////}
}
