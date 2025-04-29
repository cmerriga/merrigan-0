using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    // For when you have a way to get s indexed by integer, and want to call it an array.
    [Untested]
    internal class CustomArray<T> : Array<T> {
        private Func<long, T> getItem;
        private Func<long> getLength;

        public override long Length { get { return getLength(); } }

        public CustomArray(Func<long, T> getItem, Func<long> getLength) : this(getItem, getLength, null, false) { }

        public CustomArray(Func<long, T> getItem, Func<long> getLength, Func<T, T, int> compare, bool distinct)
            : base(compare, distinct) {
            this.getItem = getItem;
            this.getLength = getLength;
        }

        public override bool TryGetItem(long i, out T item) {
            if (i >= getLength()) {
                item = default(T);
                return false;
            }
            item = getItem(i);
            return true;
        }

        //protected override T GetItem(long i) { return getItem(i); }
    }
}
