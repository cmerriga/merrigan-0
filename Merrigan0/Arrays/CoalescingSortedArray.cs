using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    //// Possibly all arrays needing coalesce should use the plain CoalescingArray, but ensure that
    //// the items are sorted and/or distinct as needed.
    ////[Untested]
    ////internal abstract class CoalescingSortedArray<T> : Array<T> {
    ////    protected MutableSortedArray<T> CoalescedItemsSoFar;
    ////    protected bool fullyCoalesced;

    ////    public CoalescingSortedArray(Func<T, T, int> compare) : base(compare) {
    ////        CoalescedItemsSoFar = new MutableSortedArray<T>(compare);
    ////    }

    ////    public override long Length {
    ////        get {
    ////            if (!fullyCoalesced) {
    ////                if (CoalescedItemsSoFar == null) {
    ////                    CoalescedItemsSoFar = new MutableArray<T>();
    ////                }
    ////                TryCoalesceUntil(int.MaxValue);
    ////                fullyCoalesced = true;
    ////            }
    ////            return CoalescedItemsSoFar.Current.Length;
    ////        }
    ////    }

    ////    protected bool TryCoalesceUntil(long i) {
    ////        long nAlreadyCoalesced = CoalescedItemsSoFar.Current.Length;
    ////        while (nAlreadyCoalesced <= i) {
    ////            T item;
    ////            if (!TryGetNextUncoalescedItem(out item)) {
    ////                return false;
    ////            }
    ////            CoalescedItemsSoFar.Add(item);
    ////        }
    ////        return true;
    ////    }

    ////    protected abstract bool TryGetNextUncoalescedItem(out T item);

    ////    protected override T GetItem(long i) {
    ////        if (CoalescedItemsSoFar == null) {
    ////            CoalescedItemsSoFar = new MutableArray<T>();
    ////        }
    ////        if (!fullyCoalesced && i >= CoalescedItemsSoFar.Current.Length) {
    ////            if (!TryCoalesceUntil(i)) {
    ////                throw new IndexOutOfRangeException();
    ////            }
    ////        }
    ////        return CoalescedItemsSoFar.Current[i];
    ////    }
    ////}
}
