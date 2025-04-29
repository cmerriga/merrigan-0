using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // Wrapper for a sortedDistinctBlock. Use when you have a sortedDistinctBlock but need an Array.
    [Untested]
    internal class BlockWrapperArray<T> : Array<T> {
        private T[] block;

        public override long Length { get { return block.LongLength; } }

        public BlockWrapperArray(T[] block) : this(block, null, false) { }

        public BlockWrapperArray(T[] block, Func<T, T, int> compare, bool distinct) :
            base(compare, distinct) 
        { 
            this.block = block; 
        }

        /// <summary>
        /// Copies the elements of the ICollection<T> to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied from ICollection<T>. The Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="ArgumentNullException">array is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        public override void CopyTo(T[] array, int arrayIndex) { block.CopyTo(array, arrayIndex); }

        public override Array<T> Subarray(long i, long length) {
            return new SubblockWrapperArray<T>(block, i, length);
        }

        public override T[] ToBlock() { return block; }
        ////public override ICollection<T> ToICollection() { return block; }
        ////public override IEnumerable<T> ToIEnumerable() { return block; }
        ////public override IList<T> ToIList() { return block; }

        public override bool TryGetItem(long i, out T item) {
            if (i >= block.Length) {
                item = default(T);
                return false;
            }
            item = block[i];
            return true;
        }
    }
}
