using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    ////// A lazily-populated sorted array based on a filter over another sorted array.
    ////[Untested]
    ////internal class FilteredSortedArray<T> : SortedArray<T> {
    ////    private SortedArray<T> baseSortedItems;
    ////    private Func<T, bool> condition;
    ////    private MutableArray<T> filteredSortedItems;
    ////    private int iFilteredUntil;

    ////    public override long Length {
    ////        get {
    ////            if (iFilteredUntil < baseSortedItems.Length) {
    ////                FilterUntil(long.MaxValue);
    ////            }
    ////            return baseSortedItems.Length;
    ////        }
    ////    }

    ////    public FilteredSortedArray(SortedArray<T> sortedItems, Func<T, bool> condition) : base(sortedItems.Compare) {
    ////        baseSortedItems = sortedItems;
    ////        this.condition = condition;
    ////        filteredSortedItems = new MutableArray<T>();
    ////    }

    ////    protected override T GetItem(long i) {
    ////        if (i >= filteredSortedItems.Current.Length) {
    ////            FilterUntil(i);
    ////        }
    ////        return filteredSortedItems.Current[i];
    ////    }

    ////    private void FilterUntil(long i) {
    ////        long length = baseSortedItems.Length;
    ////        while (iFilteredUntil < length) {
    ////            T item = baseSortedItems[iFilteredUntil];
    ////            if (condition(item)) {
    ////                filteredSortedItems.Append(item);
    ////                if (i < filteredSortedItems.Current.Length) {
    ////                    return;
    ////                }
    ////            }
    ////            ++iFilteredUntil;
    ////        }
    ////    }
    ////}
}
