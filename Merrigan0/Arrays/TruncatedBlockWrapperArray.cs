using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // When you've got a [] but need an Array.
    // If the original [] changes, this Array will change too.
    // Recommended: nullify any reference to the [] just after creating this Array.
    [Untested]
    internal class TruncatedBlockWrapperArray<T> : Array<T> {
        private T[] items;

        //[LessOrEqual("items.Count")]
        private long length;
        
        public override long Length { get { return length; } }

        public TruncatedBlockWrapperArray(T[] items, /*[LessOrEqual("items.Count")]*/ long length) :
            this(items, length, null, false)
        {
        }

        public TruncatedBlockWrapperArray(T[] items, /*[LessOrEqual("items.Count")]*/ long length, Func<T, T, int> compare, bool distinct) :
            base(compare, distinct) {
            this.items = items;
            this.length = length;
        }

        public override bool TryGetItem(long i, out T item) {
            if (i >= length) {
                item = default(T);
                return false;
            }
            item = items[i];
            return true;
        }

        //protected override T GetItem(/*[Index] */ long i) {
        //    Utilities.ThrowIfIndexOutOfRange(i, length);
        //    return items[i];
        //}
    }
}
