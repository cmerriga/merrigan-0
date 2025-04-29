using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    [Untested]
    internal class ConcatenateArray<T> : Array<T> {
        private Array<T> array1;
        private Array<T> array2;

        public override long Length { get { return array1.Length + array2.Length; } }

        public ConcatenateArray(Array<T> array1, Array<T> array2, Func<T, T, int> compare, bool distinct) :
            base(array1.CompareFunction, array1.IsDistinct) {
            this.array1 = array1;
            this.array2 = array2;
        }

        public ConcatenateArray(Array<T> array1, Array<T> array2) :
            this(array1, array2, null, false)
        { 
        }

        /// <summary>
        /// Copies the elements of the ICollection<T> to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied from ICollection<T>. The Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="ArgumentNullException">array is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        public override void CopyTo(T[] array, int arrayIndex) {
            array1.CopyTo(array, arrayIndex);
            array2.CopyTo(array, arrayIndex + (int)array1.Length);
        }

        ///// Special concatenated enumerator?

        public override bool TryGetItem(long i, out T item) {
            long array1Length = array1.Length;
            if (i < array1Length) {
                return array1.TryGetItem(i, out item);
            }
            long iInArray2 = i - array1Length;
            return array2.TryGetItem(iInArray2, out item);
        }

        ////protected static Func<T, T, int> GetCommonComparer(Array<T> items1, Array<T> items2) {
        ////    if (items1.Compare != null && Object.ReferenceEquals(items1.Compare, items2.Compare)) {
        ////        if (items1.Length == 0 || items2.Length == 0) {
        ////            return items1.Compare;
        ////        }
        ////        if (items1.Compare(items1.Last(), items2[0]) <= 0) {
        ////            return items1.Compare;
        ////        }
        ////    }
        ////    return null;

        ////}
    }
}
