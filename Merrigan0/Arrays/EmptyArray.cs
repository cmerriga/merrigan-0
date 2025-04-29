using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // An array with no s.
    [Untested]
    internal class EmptyArray<T> : Array<T> {
        public override long Length { get { return 0; } }

        public override bool TryGetItem(long i, out T item) {
            item = default(T);
            return false;
        }

        //protected override T GetItem(long i) { throw new IndexOutOfRangeException(); }
    }
}
