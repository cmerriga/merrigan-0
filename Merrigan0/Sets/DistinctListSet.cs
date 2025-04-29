using System;
using System.Collections.Generic;

namespace Merrigan0.SetsInternal {
    ////// Use DistinctArraySet(DistinctListArray) instead.
    ////// Use when you have a distinct list and want a set.
    ////// If the original list changes, this set will change too.
    ////// Recommended: nullify any reference to the list just after creating this set.
    ////// During default execution, the distinctness is not verified. Callers must ensure that the list is distinct.
    ////[Untested]
    ////internal class DistinctListSet<T> : Set<T> {
    ////    private IList<T> distinctItems;
    ////    private Dictionary<T, long> indexesByItem;

    ////    public override long Length { get { return distinctItems.Count; } }

    ////    public DistinctListSet(IList<T> distinctItems) { this.distinctItems = distinctItems; }

    ////    public override bool Contains(T item) {
    ////        if (indexesByItem == null) {
    ////            indexesByItem = CreateIndexesByItem(this.distinctItems);
    ////        }
    ////        return indexesByItem.ContainsKey(item);
    ////    }

    ////    /// <summary>
    ////    /// Copies the elements of the ICollection<T> to an Array, starting at a particular Array index.
    ////    /// </summary>
    ////    /// <param name="loadedItems">The one-dimensional Array that is the destination of the elements copied from ICollection<T>. The Array must have zero-based indexing.</param>
    ////    /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
    ////    /// <exception cref="ArgumentNullException">array is null.</exception>
    ////    /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
    ////    public override void CopyTo(T[] array, int arrayIndex) { distinctItems.CopyTo(array, arrayIndex); }

    ////    public override IEnumerator<T> GetEnumerator() {
    ////        return (IEnumerator<T>)distinctItems.GetEnumerator();
    ////    }

    ////    ////public override T[] ToBlock() { return sortedDistinctBlock; }
    ////    ////public override ICollection<T> ToICollection() { return sortedDistinctBlock; }
    ////    ////public override IEnumerable<T> ToIEnumerable() { return sortedDistinctBlock; }
    ////    ////public override IList<T> ToIList() { return sortedDistinctBlock; }

    ////    protected static Dictionary<T, long> CreateIndexesByItem(IList<T> distinctItems) {
    ////        Dictionary<T, long> indexesByItemSoFar = new Dictionary<T, long>(distinctItems.Count);
    ////        int distinctItemsLength = distinctItems.Count;
    ////        for (int i = 0; i < distinctItemsLength; ++i) {
    ////            indexesByItemSoFar.Add(distinctItems[i], i);
    ////        }
    ////        return indexesByItemSoFar;
    ////    }
    ////}
}
