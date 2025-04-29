using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // When you've got a [] but need an Array.
    // If the original [] changes, this Array will change too.
    // Recommended: nullify any reference to the [] just after creating this Array.
    //[Always("(i + length) in-range (0, items.Count)")]
    [Untested]
    internal class SubblockWrapperArray<T> : Array<T> {
        private T[] items;

        //[InRange(0, "items.Length")]
        private long i;

        private long length;
        
        public override long Length { get { return length; } }

        //[Always("(i + length) in-range \"\\[0, items.Count)\\]\"")]
        public SubblockWrapperArray(T[] items, /* [Index("items.Count")]*/ long i, /* [InRange(0, "items.Length")]*/ long length) 
            : this(items, i, length, null, false) {
        }

        public SubblockWrapperArray(T[] items, /* [Index("items.Count")]*/ long i, /* [InRange(0, "items.Length")]*/ long length, Func<T, T, int> compare, bool distinct)
            : base(compare, distinct) {
            this.items = items;
            this.i = i;
            this.length = length;
        }

        public override bool TryGetItem(long i, out T item) {
            if (i >= length) {
                item = default(T);
                return false;
            }
            item = items[this.i + i];
            return true;
        }

        //protected override T GetItem(/*[Index] */long i) {
        //    Utilities.ThrowIfIndexOutOfRange(i, length);
        //    return items[this.i + i];
        //}
    }
}
