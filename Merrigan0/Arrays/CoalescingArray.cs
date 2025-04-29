using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // Implementations of coalescing arrays need only provide a TryGetNextUncoalescedItem method that
    // returns each item of the array, in order, beginning with the first.
    [Untested]
    internal abstract class CoalescingArray<T> : Array<T> {
        protected MutableArray<T> CoalescedItemsSoFar;
        protected bool fullyCoalesced;

        protected CoalescingArray() : this(null, false) { }

        protected CoalescingArray(Func<T, T, int> compare, bool distinct)
            : base(compare, distinct) {
        }

        public override long Length {
            get {
                if (!fullyCoalesced) {
                    if (CoalescedItemsSoFar == null) {
                        CoalescedItemsSoFar = new MutableArray<T>();
                    }
                    TryCoalesceUntil(int.MaxValue);
                    fullyCoalesced = true;
                }
                return CoalescedItemsSoFar.Current.Length;
            }
        }

        public override bool TryGetItem(long i, out T item) {
            if (CoalescedItemsSoFar == null) {
                CoalescedItemsSoFar = new MutableArray<T>();
            }
            if (!fullyCoalesced && i >= CoalescedItemsSoFar.Current.Length) {
                if (!TryCoalesceUntil(i)) {
                    item = default(T);
                    return false;
                }
            }
            if (i >= CoalescedItemsSoFar.Current.Length) {
                item = default(T);
                return false;
            }
            item = CoalescedItemsSoFar.Current[i];
            return true;
        }

        protected bool TryCoalesceUntil(long i) {
            long nAlreadyCoalesced = CoalescedItemsSoFar.Current.Length;
            while (nAlreadyCoalesced <= i) {
                T item;
                if (!TryGetNextUncoalescedItem(out item)) {
                    return false;
                }
                CoalescedItemsSoFar.Append(item);
                ++nAlreadyCoalesced;
            }
            return true;
        }

        protected abstract bool TryGetNextUncoalescedItem(out T item);
    }

    //[Untested]
    //internal class ExampleArray<T> : CoalescingArray<T> {
    //    protected ExampleArray(Func<T, T, int> compare, bool distinct)
    //        : base(compare, distinct) {
    //    }

    //    protected override bool TryGetNextUncoalescedItem(out T item);
    //}
}
