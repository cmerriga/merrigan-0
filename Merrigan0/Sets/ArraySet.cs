using System;
using System.Collections.Generic;

namespace Merrigan0.SetsInternal {
    // Use when you have a not-necessarily-distinct array, but need a set.
    // Use DistinctArraySet with a previously distilled array.
    [Untested]
    internal class ArraySet<T> : Set<T> {
        private Array<T> distinctBaseArray;
        private Dictionary<T, long> indexesByItem;

        public override long Length { get { return indexesByItem.Count; } }

        public ArraySet(Array<T> baseArray) {
            this.distinctBaseArray = baseArray.Distinct();
        }

        //// need to change this to a sorted one, maybe a SortedCoalescingSet with a MutableSortedArray
        public override bool Contains(T item) {
            if (indexesByItem == null) {
                indexesByItem = CreateIndexesByItem(this.distinctBaseArray);
            }
            return indexesByItem.ContainsKey(item);
        }

        public override IEnumerator<T> GetEnumerator() {
            if (indexesByItem == null) {
                indexesByItem = CreateIndexesByItem(this.distinctBaseArray);
            }
            return indexesByItem.Keys.GetEnumerator();
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
