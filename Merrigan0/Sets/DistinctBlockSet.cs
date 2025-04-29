using System;
using System.Collections.Generic;

namespace Merrigan0.SetsInternal {
    ////// Use DistinctArraySet(DistinctBlockArray) instead.
    ////// Use when you have a sorted distinct [] and want a set.
    ////// If the original [] changes, this set will change too.
    ////// Recommended: nullify any reference to the [] just after creating this set.
    ////// During default execution, the distinctness is not verified. Callers must ensure that the block is distinct.
    ////[Untested]
    ////internal class DistinctBlockSet<T> : Set<T> {
    ////    private T[] distinctItems;
    ////    private Dictionary<T, long> indexesByItem;

    ////    public override long Length { get { return distinctItems.LongLength; } }

    ////    public DistinctBlockSet(T[] distinctItems) { this.distinctItems = distinctItems; }

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

    ////    protected static Dictionary<T, long> CreateIndexesByItem(T[] distinctItems) {
    ////        Dictionary<T, long> indexesByItemSoFar = new Dictionary<T, long>((int)distinctItems.Length);
    ////        long distinctItemsLength = distinctItems.LongLength;
    ////        for (long i = 0; i < distinctItemsLength; ++i) {
    ////            indexesByItemSoFar.Add(distinctItems[i], i);
    ////        }
    ////        return indexesByItemSoFar;
    ////    }
    ////}
}
