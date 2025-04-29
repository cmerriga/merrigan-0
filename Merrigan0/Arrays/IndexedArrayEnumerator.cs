using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // A basic enumerator for any array, beginning at a particular index.
    [Untested]
    internal class IndexedArrayEnumerator<T> : Enumerator<T> {
        private long i;
        private long iBegin;
        private Array<T> items;
        private T current;

        protected override T UnsafeCurrent { get { return current; } }

        public IndexedArrayEnumerator(Array<T> items, long iBegin) {
            this.items = items;
            this.iBegin = iBegin;
        }

        public override Boolean MoveNext() {
            ++i;
            if (i >= items.Length) {
                return false;
            }
            current = items[i];
            return true;
        }

        protected override void UnsafeReset() { i = iBegin - 1; }
    }
}
