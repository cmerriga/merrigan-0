using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // Wraps any array and sorts as needed using heapsort.
    [Untested]
    internal class ArraySortedDistinctArray<T> : CoalescingArray<T> {
        private Heap<T> heap;

        public ArraySortedDistinctArray(Array<T> items, Func<T, T, int> compare)
            : base(compare, true) {
            heap = new Heap<T>(compare);
            foreach (T item in items) {
                heap.Add(item);
            }
        }

        ////public override IEnumerable<T> ToIEnumerable() { return this; }
        ////public override IList<T> ToIList() { return new ArrayList<T>(this); }

        protected override bool TryGetNextUncoalescedItem(out T item) {
            while (true) {
                T itemToTry;
                if (!heap.TryPop(out itemToTry)) {
                    break;
                }
                if (!CoalescedItemsSoFar.Current.Contains(itemToTry)) {
                    item = itemToTry;
                    return true;
                }
            }
            item = default(T);
            return true;
        }
    }
}
