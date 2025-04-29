using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // Wraps any array and distills as needed.
    [Untested]
    internal class ArrayDistinctArray<T> : CoalescingArray<T> {
        private Array<T> baseItems;
        private long iNextUncoalescedItem;

        public ArrayDistinctArray(Array<T> baseItems) : base(null, true) {
            this.baseItems = baseItems;
        }

        protected override bool TryGetNextUncoalescedItem(out T item) {
            long baseItemsLength = baseItems.Length;
            while (iNextUncoalescedItem < baseItemsLength) {
                T itemToTry = baseItems[iNextUncoalescedItem];
                if (!CoalescedItemsSoFar.Current.Contains(itemToTry)) {
                    item = itemToTry;
                    return true;
                }
                ++iNextUncoalescedItem;
            }
            item = default(T);
            return false;
        }
    }
}
