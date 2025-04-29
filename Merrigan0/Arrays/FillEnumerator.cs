using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // Enumerator that can be applied to any filled array, that just uses the index operator.
    [Untested]
    internal class FillEnumerator<T> : IEnumerator<T> {
        private long iNext;
        private long length;
        private T item;
        private T current;

        public T Current { get { return current; } }

        Object IEnumerator.Current { get { return Current; } }

        public FillEnumerator(long length, T item) {
            this.length = length;
            this.item = item;
        }

        public void Dispose() { }

        public Boolean MoveNext() {
            if (iNext >= length) { return false; }
            current = item;
            ++iNext;
            return true;
        }
    
        public void Reset() { 
            iNext = 0;
            current = default(T);
        }
    }
}
