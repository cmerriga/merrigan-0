using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    //[Untested]
    //public class MutableArray2D<T> /*: Array<T>*/ {
    //    public static MutableArray2D<T> operator +(MutableArray2D<T> a, T item) {
    //        a.Append(item);
    //        return a;
    //    }

    //    public Array2D<T> Current { get; protected set; }

    //    public MutableArray2D(Array2D<T> baseItems) { Current = baseItems; }
    //    public MutableArray2D() : this(Array2D<T>.Empty) { }

    //    public void Append(T item) { Current = new AppendArray2D<T>(Current, item); }
    //    public void Append(Array<T> items) { Current = new ConcatenateArray2D<T>(Current, items); }
    //    public void AppendRow(Array<T> items) { Current = new ConcatenateArray2D<T>(Current, items); }
    //    public void AppendRows(Array<Array<T>> items) { Current = new ConcatenateArray2D<T>(Current, items); }
    //    public void Insert(long r, long i, T item) { Current = new InsertItemArray<T>(Current, i, item); }
    //    public void Insert(long r, long i, Array<T> items) { Current = new InsertArray<T>(Current, i, items); }
    //    public void InsertRows(long r, Array<Array<T>> rows) { Current = new InsertArray<T>(Current, i, items); }
    //    public void Prepend(T item) { Current = new PrependArray<T>(Current, item); }
    //    public void Prepend(Array<T> items) { Current = new ConcatenateArray<T>(items, Current); }
    //    public void PrependRow(Array<T> items) { Current = new ConcatenateArray<T>(items, Current); }
    //    public void RemoveRows(long r, long i, long length) { Current = new RemoveArray<T>(Current, i, length); }
    //    public void RemoveRows(long r, long h) { Current = new RemoveArray<T>(Current, i, length); }
    //    public override string ToString() { return Current.ToString(); }
    //    public void TruncateRows(long n) { Current = Current.Subarray(0, Current.Length - n); }
    //    public void TruncateRow(long r, long n) { Current = Current.Subarray(0, Current.Length - n); }
    //}
}
