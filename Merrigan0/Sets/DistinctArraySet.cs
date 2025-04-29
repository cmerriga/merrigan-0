using System;
using System.Collections.Generic;

namespace Merrigan0.SetsInternal {
    // Use when you have an Array happening to have distinct items, but need a Set.
    // When the first Contains operation is invoked, the items are place in a dictionary for
    // easy identification
    [Untested]
    internal class DistinctArraySet<T> : Set<T> {
        private Array<T> distinctArray;
        private Dictionary<T, long> indexesByItem;

        public override long Length { get { return distinctArray.Length; } }

        public DistinctArraySet(Array<T> distinctItems) {
            this.distinctArray = distinctItems;
        }

        //// need to change this to a sorted one, maybe a SortedCoalescingSet with a MutableSortedArray
        public override bool Contains(T item) {
            if (indexesByItem == null) {
                indexesByItem = CreateIndexesByItem(this.distinctArray);
            }
            return indexesByItem.ContainsKey(item);
        }

        public override IEnumerator<T> GetEnumerator() {
            if (indexesByItem == null) {
                indexesByItem = CreateIndexesByItem(this.distinctArray);
            }
            return distinctArray.GetEnumerator();
        }

        protected static Dictionary<T, long> CreateIndexesByItem(Array<T> distinctItems) {
            Dictionary<T, long> indexesByItemSoFar = new Dictionary<T, long>((int)distinctItems.Length);
            long distinctItemsLength = distinctItems.Length;
            for (long i = 0; i < distinctItemsLength; ++i) {
                indexesByItemSoFar.Add(distinctItems[i], i);
            }
            return indexesByItemSoFar;
        }
    }
}
