using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // When you've got an IList but need an Array.
    // If the original list changes, this Array will change too.
    // Recommended: nullify any reference to the IList just after creating this Array.
    //[Always("(i + length) in-range (0, items.Count)")]
    [Untested]
    internal class TruncatedListWrapperArray<T> : Array<T> {
        private IList<T> items;
        
        //[LessOrEqual("items.Count")]
        private long length;
        
        public override long Length { get { return length; } }
        
        public TruncatedListWrapperArray(IList<T> items, /*[LessOrEqual("items.Count")]*/ long length) :
            this(items, length, null, false) {
        }

        public TruncatedListWrapperArray(IList<T> items, /*[LessOrEqual("items.Count")]*/ long length, Func<T, T, int> compare, bool distinct)
            : base(compare, distinct) {
            this.items = items;
            this.length = length;
        }

        public override bool TryGetItem(long i, out T item) {
            if (i >= length) {
                item = default(T);
                return false;
            }
            item = items[(int)i];
            return true;
        }

        //protected override T GetItem(/*[LessOrEqual("items.Count", typeof(IndexOutOfRangeException))]*/ long i) {
        //    Utilities.ThrowIfIndexOutOfRange(i, length);
        //    return items[(int)i];
        //}
    }
}
