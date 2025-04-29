using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // An array filled with all the same value.
    [Untested]
    internal class FillArray<T> : Array<T> {
        private T item;
        private long length;

        public override long Length { get { return length; } }

        public T Value { get; private set; }

        public FillArray(T item, long length) {
            this.item = item;
            this.length = length;
        }

        public override IEnumerator<T> GetEnumerator() { return new FillEnumerator<T>(length, item); }

        public override bool TryGetItem(long i, out T item) {
            item = this.item;
            return true;
        }
    }
}
