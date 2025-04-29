using System;
using System.Collections.Generic;

namespace Merrigan0.SetsInternal {
    ////// Use when you have a SortedArray but need a Set.
    ////[Untested]
    ////internal class SortedArraySet<T> : CoalescingSet<T> {
    ////    private SortedArray<T> sortedItems;
    ////    private long sortedItemsLength;
    ////    private long nTriedFromSortedItems;

    ////    public override long Length { get { return sortedItems.Length; } }

    ////    public SortedArraySet(SortedArray<T> sortedItems) {
    ////        this.sortedItems = sortedItems;
    ////        sortedItemsLength = this.sortedItems.Length;
    ////    }

    ////    //// need to change this to a sorted one, maybe a SortedCoalescingSet with a MutableSortedArray
    ////    public override bool Contains(T value) { return coalescedItems.Current.Contains(value); }

    ////    protected override bool TryGetNextUncoalescedItem(out T item) {
    ////        while (true) {
    ////            if (nTriedFromSortedItems >= sortedItemsLength) {
    ////                item = default(T);
    ////                return false;
    ////            }
    ////            T itemToTry = sortedItems[nTriedFromSortedItems];
    ////            ++nTriedFromSortedItems;
    ////            if (!coalescedItems.Current.Contains(itemToTry)) {
    ////                item = itemToTry;
    ////                break;
    ////            }
    ////        }
    ////        return true;
    ////    }
    ////}
}
