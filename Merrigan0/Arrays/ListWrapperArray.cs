using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // Use when you have an IList but need an Array on a temporary basis.
    // If the original IList changes, this Array will change too.
    // Recommended: nullify any reference to the IList just after creating this Array.
    [Untested]
    internal class ListWrapperArray<T> : Array<T> {
        //[NotNull]
        private IList<T> items;

        /// <summary>
        /// Gets the number of distinctElements contained in the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <value>The number of distinctElements contained in the <see cref="ICollection{T}"/>.</value>
        public override long Length { get { return items.Count; } }

        public ListWrapperArray(IList<T> items) : this(items, null, false) { }

        public ListWrapperArray(IList<T> items, Func<T, T, int> compare, bool distinct) :
            base(compare, distinct) 
        { 
            this.items = items; 
        }

        public override void CopyTo(T[] array, int arrayIndex) { items.CopyTo(array, (int)arrayIndex); }
        ////public override ICollection<T> ToICollection() { return items; }
        ////public override IEnumerable<T> ToIEnumerable() { return items; }
        ////public override IList<T> ToIList() { return items; }

        public override bool TryGetItem(long i, out T item) {
            if (i >= items.Count) {
                item = default(T);
                return false;
            }
            item = items[(int)i];
            return true;
        }

        //protected override T GetItem(long i) { return items[(int)i]; }
    }
}
