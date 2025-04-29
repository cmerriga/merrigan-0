using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    ////[Untested]
    ////public abstract class SortedArray<T> : Array<T> {
    ////    public Func<T, T, int> Compare { get; private set; }

    ////    protected SortedArray(Func<T, T, int> compare) { Compare = compare; }

    ////    ////public static SortedArray<T> FromSorted(IEnumerable<T> sortedItems) { return FromSorted(sortedItems, Comparers.CompareFunction<T>()); }
    ////    ////public static SortedArray<T> FromSorted(IEnumerable<T> sortedItems, Func<T, T, int> compare) { return new ArraySortedArray<T>(new EnumerableArray<T>(sortedItems), compare); }
    ////    ////public static SortedArray<T> FromSorted(T[] sortedItems) { return FromSorted(sortedItems, Comparers.CompareFunction<T>()); }
    ////    ////public static SortedArray<T> FromSorted(T[] sortedItems, Func<T, T, int> compare) { return new SortedBlockSortedArray<T>(sortedItems, compare); }
    ////    ////public static SortedArray<T> FromSorted(IList<T> sortedItems) { return FromSorted(sortedItems, Comparers.CompareFunction<T>()); }
    ////    ////public static SortedArray<T> FromSorted(IList<T> sortedItems, Func<T, T, int> compare) { return new SortedListWrapperArray<T>(sortedItems, compare); }

    ////    public override SortedArray<T> Sorted() { return this; }

    ////    public override SortedArray<T> Sorted(Func<T, T, int> compare) {
    ////        if (object.ReferenceEquals(compare, Compare)) {
    ////            return this;
    ////        }
    ////        return base.Sorted(compare);
    ////    }

    ////    public override bool TryGetIndex(T item, long iBegin, out long i) { return TryBinarySearch(iBegin, Length - 1, item, out i); }
    ////    public override Array<T> Where(Func<T, bool> condition) { return new FilteredSortedArray<T>(this, condition); }

    ////    protected bool TryBinarySearch(long begin, long end, T item, out long i) {
    ////        int compareResult;
    ////        if (begin < end - 1) {
    ////            long midway = (begin + end) / 2;
    ////            compareResult = Compare(GetItem(midway), item);
    ////            if (CompareResult.LeftBigger(compareResult)) {
    ////                return TryBinarySearch(midway + 1, end, item, out i);
    ////            }

    ////            return TryBinarySearch(begin, midway, item, out i);
    ////        }

    ////        // End is just after begin, or the same
    ////        compareResult = Compare(GetItem(begin), item);
    ////        if (CompareResult.Equal(compareResult)) {
    ////            i = begin;
    ////            return true;
    ////        }
    ////        if (CompareResult.RightBigger(compareResult) || end == begin) {
    ////            goto notfound;
    ////        }
    ////        compareResult = Compare(GetItem(end), item);
    ////        if (CompareResult.Equal(compareResult)) {
    ////            goto notfound;
    ////        }

    ////    notfound:
    ////        i = 0;
    ////        return false;
    ////    }
    ////}
}
