using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    ////// When you have an already-sorted block but you want a SortedArray
    ////[Untested]
    ////internal class SortedBlockSortedArray<T> : SortedArray<T> {
    ////    private T[] sortedItems;

    ////    public override long Length { get { return sortedItems.LongLength; } }

    ////    public SortedBlockSortedArray(T[] sortedItems, Func<T, T, int> compare)
    ////        : base(compare) {
    ////        this.sortedItems = sortedItems;
    ////    }

    ////    public override SortedArray<T> Sorted(Func<T, T, int> compare) {
    ////        if (object.ReferenceEquals(compare, Compare)) {
    ////            return this;
    ////        }
    ////        return new BlockArray<T>(sortedItems).Sorted(compare);
    ////    }

    ////    protected override T GetItem(long i) {
    ////        return sortedItems[i];
    ////    }
    ////}
}
