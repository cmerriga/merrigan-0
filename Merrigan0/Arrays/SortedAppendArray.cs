using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    ////// When you have an array plus an map to put on the end
    ////[Untested]
    ////internal class SortedAppendArray<T> : SortedArray<T> {
    ////    private SortedArray<T> items;
    ////    private T item;

    ////    public override long Length { get { return items.Length + 1; } }

    ////    public SortedAppendArray(SortedArray<T> items, T item) : base(items.Compare)
    ////    {
    ////        this.items = items;
    ////        this.item = item;
    ////    }

    ////    /// <summary>
    ////    /// Copies the elements of the ICollection<T> to an Array, starting at a particular Array index.
    ////    /// </summary>
    ////    /// <param name="array">The one-dimensional Array that is the destination of the elements copied from ICollection<T>. The Array must have zero-based indexing.</param>
    ////    /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
    ////    /// <exception cref="ArgumentNullException">array is null.</exception>
    ////    /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
    ////    public override void CopyTo(T[] array, int arrayIndex) {
    ////        items.CopyTo(array, arrayIndex);
    ////        array[arrayIndex + items.Length] = item;
    ////    }

    ////    protected override T GetItem(long i) {
    ////        if (i < items.Length) {
    ////            return items[i];
    ////        }
    ////        if (i > items.Length) {
    ////            throw new IndexOutOfRangeException();
    ////        }
    ////        return item;
    ////    }
    ////}
}
