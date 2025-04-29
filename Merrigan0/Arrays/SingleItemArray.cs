using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    [Untested]
    internal class SingleItemArray<T> : Array<T> {
        private T item;

        public override long Length { get { return 1; } }

        public SingleItemArray(T item) { this.item = item; }

        public override IEnumerator<T> GetEnumerator() { return new SingleItemEnumerator<T>(item); }

        public override bool TryGetItem(long i, out T item) {
            if (i == 0) {
                item = this.item;
                return true;
            }
            item = default(T);
            return false;
        }

        //protected override T GetItem(long i) {
        //    if (i != 0) {
        //        throw new IndexOutOfRangeException();
        //    }
        //    return item;
        //}
    }
}
