using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    [Untested]
    internal class TruncatedArray<T> : Array<T> {
        private Array<T> baseItems;

        //[LessOrEqual("items.Count")]
        private long length;
        
        public override long Length { get { return length; } }

        public TruncatedArray(Array<T> baseItems, /*[LessOrEqual("items.Count")]*/ long length) :
            base(baseItems.CompareFunction, baseItems.IsDistinct)
        {
            this.baseItems = baseItems;
            this.length = length;
        }

        public override bool TryGetItem(long i, out T item) {
            if (i >= length) {
                item = default(T);
                return false;
            }
            return baseItems.TryGetItem(i, out item);
        }

        //protected override T GetItem(/*[Index] */ long i) {
        //    Utilities.ThrowIfIndexOutOfRange(i, length);
        //    return baseItems[i];
        //}
    }
}
