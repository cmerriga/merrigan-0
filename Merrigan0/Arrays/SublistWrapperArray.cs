using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // When you've got an IList but need an Array.
    // If the original IList changes, this Array will change too.
    // Recommended: nullify any reference to the IList just after creating this Array.
    //[Always("(i + length) in-range (0, items.Count)")]
    [Untested]
    internal class SublistWrapperArray<T> : Array<T> {
        private IList<T> items;

        //[InRange(0, "items.Length")]
        private long i;

        private long length;
        
        public override long Length { get { return length; } }

        public SublistWrapperArray(IList<T> items, /* [Index("items.Count")]*/ long i, /* [InRange(0, "items.Length")]*/ long length) 
            : this(items, i, length, null, false) {
        }

       //[Always("(i + length) in-range \"\\[0, items.Count)\\]\"")]
        public SublistWrapperArray(IList<T>  items, /* [Index("items.Count")]*/ long i, /* [InRange(0, "items.Length")]*/ long length, Func<T, T, int> compare, bool distinct)
            : base(compare, distinct) {
            this.items = items;
            this.i = i;
            this.length = length;
        }

        public override bool TryGetItem(/*[Index] */long i, out T item) {
            if (i >= items.Count) {
                item = default(T);
                return false;
            }
            item = items[(int)(this.i + i)];
            return true;
        }
    }
}
