using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    [Untested]
    internal class InsertArray<T> : Array<T> {
        private Array<T> baseArray;
        private long i;
        private Array<T> insertedArray;

        public override long Length { get { return baseArray.Length + insertedArray.Length; } }

        public InsertArray(Array<T> baseArray, long i, Array<T> insertedArray)
        { 
            // If the value is not compliant with the distinctness and sort compareResult, keep them
            ////if (baseArray.Length > 0) {
            ////    if (Compare != null) {
            ////        if (Compare(item, baseArray[0]) > 0) {
            ////            throw new ArgumentException();
            ////        }
            ////    }
            ////    if (IsDistinct) {
            ////        if (baseArray.Contains(item)) {
            ////            throw new ArgumentException();
            ////        }
            ////    }
            ////}

            this.baseArray = baseArray;
            this.i = i;
            this.insertedArray = insertedArray;
        }

        public override void CopyTo(T[] array, int arrayIndex) {
            baseArray.Subarray(0, i).CopyTo(array, arrayIndex);
            insertedArray.CopyTo(array, arrayIndex + (int)i);
            baseArray.Subarray(i, baseArray.Length - i).CopyTo(array, arrayIndex + (int)insertedArray.Length);
        }

        ///// Special concatenated enumerator?

        public override bool TryGetItem(long i, out T item) {
            long baseArrayLength = baseArray.Length;
            if (i < baseArrayLength) {
                return baseArray.TryGetItem(i, out item);
            }
            i -= baseArrayLength;
            long insertedArrayLength = insertedArray.Length;
            if (i < insertedArrayLength) {
                return insertedArray.TryGetItem(i, out item);
            }
            return baseArray.TryGetItem(i - insertedArrayLength, out item);
        }

        //protected override T GetItem(long i) {
        //    if (i < baseArray.Length) {
        //        return baseArray[i];
        //    }
        //    i -= baseArray.Length;
        //    if (i < insertedArray.Length) {
        //        return insertedArray[i];
        //    }
        //    return baseArray[i - insertedArray.Length];
        //}
    }
}
