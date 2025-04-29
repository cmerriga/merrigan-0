using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // Use when you have an ICollection but need an Array.
    // An ICollection is an IEnumerable where you already know the length.
    [Untested]
    internal class CollectionWrapperArray<T> : EnumerableWrapperArray<T> {
        private long length;

        public override long Length { get { return length; } }

        public CollectionWrapperArray(ICollection<T> collection) : this(collection, null, false) { }

        public CollectionWrapperArray(ICollection<T> collection, Func<T, T, int> compare, bool distinct) :
            base(collection, compare, distinct) {
            length = collection.Count;
        }
    }
}
