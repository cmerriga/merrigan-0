using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    //// For when you have a Array that happens to be sorted, but need a SortedArray.
    //[Untested]
    //internal class SortedArrayArray<T> : SortedArray<T> {
    //    private Array<T> sortedItems;

    //    public override long Length { get { return sortedItems.Length; } }

    //    public SortedArrayArray(Array<T> sortedItems, Func<T, T, int> compare) : base(compare) {
    //        this.sortedItems = sortedItems;
    //    }

    //    public virtual SortedArray<T> Sorted(Func<T, T, int> compare) {
    //        if (compare == this.Compare) {
    //            return this;
    //        }
    //        return new ArraySortedArray<T>(this, compare);
    //    }

    //    protected override T GetItem(/*[LessOrEqual("items.Count", typeof(IndexOutOfRangeException))]*/ long i) {
    //        return sortedItems[i];
    //    }
    //}
}
