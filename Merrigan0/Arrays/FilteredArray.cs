using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // A lazily-populated array based on a filter over another array.
    [Untested]
    internal class FilteredArray<T> : CoalescingArray<T> {
        private Array<T> baseItems;
        private long baseItemsLength;
        private Func<T, bool> condition;
        private long iNextUncoalescedBaseItem;

        // Since the filtered array is just a similarly-ordered subset of the base array, it has the same profile
        // as the base array.
        public FilteredArray(Array<T> baseItems, Func<T, bool> condition) : base(baseItems.CompareFunction, baseItems.IsDistinct) {
            this.baseItems = baseItems;
            baseItemsLength = baseItems.Length;
            this.condition = condition;
        }

        protected override bool TryGetNextUncoalescedItem(out T item) {
            while (true) {
                if (iNextUncoalescedBaseItem >= baseItems.Length) {
                    item = default(T);
                    return false;
                }


                T itemToTry = baseItems[iNextUncoalescedBaseItem];
                ++iNextUncoalescedBaseItem;
                if (condition(itemToTry)) {
                    item = itemToTry;
                    return true;
                }
            }
        }
    }
}
