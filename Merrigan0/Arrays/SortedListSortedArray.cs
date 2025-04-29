using System;
using System.Collections.Generic;
using System.Diagnostics;
//using Merrigan0.Internal.DotNet.Extensions;

namespace Merrigan0.ArraysInternal {
    //// When you have an IList<T> that is already sorted but want a SortedArray
    //[Untested]
    //internal class SortedListSortedArray<T> : SortedArray<T> {
    //    private IList<T> sortedItems;

    //    public override long Length { get { return sortedItems.Count; } }

    //    public SortedListSortedArray(IList<T> sortedItems, Func<T, T, int> compare)
    //        : base(compare) {
    //        this.sortedItems = sortedItems;
    //    }

    //    public override Set<T> Distinct() { return new SortedArraySet<T>(this); }

    //    public override SortedArray<T> Sorted(Func<T, T, int> compare) {
    //        if (object.ReferenceEquals(compare, Compare)) {
    //            return this;
    //        } else {
    //            return sortedItems.ToArray().Sorted(compare);
    //        }
    //    }

    //    protected override T GetItem(long i) {
    //        if (i > int.MaxValue) {
    //            throw new IndexOutOfRangeException();
    //        }
    //        return sortedItems[(int)i];
    //    }
    //}
}
