using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // The standard enumerator that will work for all arrays, even if not the most efficient, using
    // indexed access.
    [Untested]
    internal class ArrayEnumerator<T> : IEnumerator<T> {
        private long iNext;
        private Array<T> items;
        private T current;

        public T Current { get { return current; } }

        Object IEnumerator.Current { get { return Current; } }

        public ArrayEnumerator(Array<T> items) { this.items = items; }

        public void Dispose() {
            items = null;
            current = default(T);
        }

        public Boolean MoveNext() {
            if (!items.TryGetItem(iNext, out current)) {
                return false;
            }
            ++iNext;
            return true;
        }
    
        public void Reset() { 
            iNext = 0;
            current = default(T);
        }
    }
}
