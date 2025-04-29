using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    [Untested]
    internal class RemoveArray<T> : Array<T> {
        private Array<T> baseArray;
        private long iRemoved;
        private long lengthRemoved;

        public override long Length { get { return baseArray.Length - lengthRemoved; } }

        public RemoveArray(Array<T> baseArray, long iRemoved, long lengthRemoved) : base(baseArray.CompareFunction, baseArray.IsDistinct)
        { 
            this.iRemoved = iRemoved;
            this.lengthRemoved = lengthRemoved;
            this.baseArray = baseArray;
        }

        /// <summary>
        /// Copies the elements of the ICollection<T> to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied from ICollection<T>. The Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="ArgumentNullException">array is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        public override void CopyTo(T[] array, int arrayIndex) {
            baseArray.Subarray(0, iRemoved).CopyTo(array, arrayIndex);
            long iBaseAfterRemoved = iRemoved + lengthRemoved;
            baseArray.Subarray(iBaseAfterRemoved).CopyTo(array, arrayIndex + (int)iRemoved);
        }

        public override bool TryGetItem(long i, out T item) {
            long iInBaseArray = (i < iRemoved) ? i : i + lengthRemoved;
            return baseArray.TryGetItem(iInBaseArray, out item);
        }

        //protected override T GetItem(long i) {
        //    if (i < iRemoved) {
        //        return baseArray[i];
        //    }
        //    return baseArray[i + lengthRemoved];
        //}
    }
}
