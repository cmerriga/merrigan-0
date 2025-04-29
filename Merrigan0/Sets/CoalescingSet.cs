using System;
using System.Collections.Generic;

namespace Merrigan0.SetsInternal {
    [Untested]
    internal abstract class CoalescingSet<T> : Set<T> {
        protected MutableMap<T, T> coalescedItems;
        protected bool fullyCoalesced;
        protected long nAlreadyCoalesced;

        public override long Length {
            get {
                if (!fullyCoalesced) {
                    if (coalescedItems == null) {
                        coalescedItems = new MutableMap<T, T>();
                    }
                    TryCoalesceUntil(int.MaxValue);
                    fullyCoalesced = true;
                }
                return coalescedItems.Current.Length;
            }
        }

        public override IEnumerator<T> GetEnumerator() {
            TryCoalesceUntil(Int64.MaxValue);
            return coalescedItems.Current.Domain.GetEnumerator();
        }

        protected bool TryCoalesceUntil(long i) {
            while (nAlreadyCoalesced <= i) {
                T item;
                if (!TryGetNextUncoalescedItem(out item)) {
                    fullyCoalesced = true;
                    return false;
                }
                coalescedItems.Add(item, item);
                ++nAlreadyCoalesced;
            }
            return true;
        }

        protected abstract bool TryGetNextUncoalescedItem(out T item);
    }
}
