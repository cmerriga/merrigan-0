using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    [Untested]
    internal class InsertItemArray<T> : Array<T> {
        private Array<T> baseArray;
        private long i;
        private T item;

        public override long Length { get { return baseArray.Length + 1; } }

        public InsertItemArray(Array<T> baseArray, long i, T item)
        { 
            // If the value is not compliant with the distinctness and sort compareResult, keep them
            if (baseArray.Length > 0) {
                if (IsDistinct) {
                    if (baseArray.Contains(item)) {
                        throw new ArgumentException();
                    }
                }
            }

            this.baseArray = baseArray;
            this.i = i;
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
            baseArray.Subarray(0, i).CopyTo(array, arrayIndex);
            array[arrayIndex + i] = item;
            baseArray.Subarray(i, baseArray.Length - i).CopyTo(array, arrayIndex + (int)i + 1);
        }

        ///// Special concatenated enumerator?

        public override bool TryGetItem(long i, out T item) {
            if (i < this.i) {
                return baseArray.TryGetItem(i, out item);
            }
            if (i > this.i) {
                return baseArray.TryGetItem(i - 1, out item);
            }
            item = this.item;
            return true;
        }

        //protected override T GetItem(long i) {
        //    if (i < this.i) {
        //        return baseArray[i];
        //    }
        //    if (i > this.i) {
        //        return baseArray[i - 1];
        //    }
        //    return item;
        //}
    }
}
