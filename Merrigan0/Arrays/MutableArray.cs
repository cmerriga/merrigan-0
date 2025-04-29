using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.ArraysInternal;

namespace Merrigan0 {
    //public interface IMutable<T> {
    //    T Current { get; }
    //}

    [Untested]
    public class MutableArray<T> /*: IMutable<T>*/ {
        //// Consider: implicit cast to Array<T>? No for now
        //public static implicit operator Array<T>(MutableArray<T> arraySoFar) {
        //    return arraySoFar.Current;
        //}
        
        public static MutableArray<T> operator +(MutableArray<T> a, T item) {
            a.Append(item);
            return a;
        }

        // Deeply coalesced
        public virtual Array<T> Current { get; protected set; }

        public MutableArray(Array<T> baseItems) { Current = baseItems; }
        public MutableArray() : this(Array<T>.Empty) { }

        public void Append(T item) { Current = new AppendArray<T>(Current, item); }
        public void Append(Array<T> items) { Current = new ConcatenateArray<T>(Current, items); }
        public void Insert(long i, T item) { Current = new InsertItemArray<T>(Current, i, item); }
        public void Insert(long i, Array<T> items) { Current = new InsertArray<T>(Current, i, items); }
        public void Prepend(T item) { Current = new PrependArray<T>(Current, item); }
        public void Prepend(Array<T> items) { Current = new ConcatenateArray<T>(items, Current); }
        public void Remove(long i, long length) { Current = new RemoveArray<T>(Current, i, length); }
        public void Sort(Func<T, T, int> compare) { Current = new ArraySortedArray<T>(Current, compare); }
        public override string ToString() { return Current.ToString(); }
        public void Truncate(long n) { Current = Current.Subarray(0, Current.Length - n); }
    }

    //[Untested]
    //public class MutableArrayOfMutable<T> : MutableArray<IMutable<T>> {
    //    // Deeply coalesced
    //    public override Array<Array<T>> Current { 
    //        get {
    //            return Current.Transform<T>((IMutable<T> from) => from.Current);
    //        }
    //    }

    //    public MutableArray(Array<IMutable<T>> baseItems) { Current = baseItems; }
    //    public MutableArray() : this(Array<T>.Empty) { }
    //}

    //public class MutableMatrix<T> : MutableArrayOfMutable<MutableArray<T> {
    //}
}
