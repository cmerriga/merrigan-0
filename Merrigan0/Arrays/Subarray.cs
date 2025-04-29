using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    [Untested]
    internal class Subarray<T> : Array<T> {
        private Array<T> baseArray;
        private long i;
        private long length;

        public override long Length { get { return length; } }

        public Subarray(Array<T> baseArray, long i, long length)
            : base(baseArray.CompareFunction, baseArray.IsDistinct) {
            this.baseArray = baseArray;
            this.i = i;
            this.length = length;
        }

        public override bool TryGetItem(long i, out T item) {
            if (i >= length) {
                item = default(T);
                return false;
            }
            return baseArray.TryGetItem(this.i + i, out item);
        }

        //protected override T GetItem(long i) {
        //    if (i >= length) {
        //        throw new IndexOutOfRangeException();
        //    }
        //    return baseArray[this.i + i];
        //}
    }
}
