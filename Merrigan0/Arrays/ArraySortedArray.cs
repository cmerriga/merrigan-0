using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // Takes any array and offers a sorted version.
    [Untested]
    internal class ArraySortedArray<T> : CoalescingArray<T> {
        private Heap<T> heap;

        public ArraySortedArray(Array<T> items) : this(items, Comparers.CompareFunction<T>()) { }

        public ArraySortedArray(Array<T> items, Func<T, T, int> compare) : base(compare, false) {
            heap = new Heap<T>(compare);
            foreach (T item in items) {
                heap.Add(item);
            }
        }

        ////public override IEnumerable<T> ToIEnumerable() { return this; }
        ////public override IList<T> ToIList() { return new ArrayList<T>(this); }

        protected override bool TryGetNextUncoalescedItem(out T item) {
            return heap.TryPop(out item);
        }
    }
}
