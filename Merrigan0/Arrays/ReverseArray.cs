using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    [Untested]
    public class ReverseArray<T> : Array<T> { //// internal
        private Array<T> baseArray;

        public override long Length { get { return baseArray.Length; } }

        public ReverseArray(Array<T> baseArray)
            : base(baseArray.CompareFunction, baseArray.IsDistinct) //// compare needs to be reversed
        { 
            this.baseArray = baseArray;
        }

        public override bool TryGetItem(long i, out T item) {
            if (i >= Length) {
                item = default(T);
                return false;
            }
            if (i < 0L) {
                throw new ArgumentOutOfRangeException();
            }
            return baseArray.TryGetItem(baseArray.Length - i - 1, out item);
        }
    }
}
