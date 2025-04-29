using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    [Untested]
    internal class PrependArray<T> : Array<T> {
        private Array<T> previous;
        private T item;

        public override long Length { get { return previous.Length + 1; } }

        public PrependArray(Array<T> previous, T item) : base(previous.CompareFunction, previous.IsDistinct)
        { 
            //// Maybe make this a debug-only correctness check
            // If the value is not compliant with the distinctness and sort compareResult, keep them
            if (previous.Length > 0) {
                if (CompareFunction != null) {
                    if (CompareFunction(item, previous[0]) > 0) {
                        throw new ArgumentException();
                    }
                }
                if (IsDistinct) {
                    if (previous.Contains(item)) {
                        throw new ArgumentException();
                    }
                }
            }

            this.previous = previous;
            this.item = item;
        }

        /// <summary>
        /// Copies the elements of the ICollection<T> to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied from ICollection<T>. The Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="ArgumentNullException">array is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        public override void CopyTo(T[] array, int arrayIndex) {
            array[0] = item;
            previous.CopyTo(array, arrayIndex + 1);
        }

        public override bool TryGetItem(long i, out T item) {
            if (i == 0) {
                item = this.item;
                return true;
            }
            return previous.TryGetItem(i - 1, out item);
        }

        //protected override T GetItem(long i) {
        //    if (i == 0) {
        //        return item;
        //    }
        //    return previous[i + 1];
        //}
    }
}
