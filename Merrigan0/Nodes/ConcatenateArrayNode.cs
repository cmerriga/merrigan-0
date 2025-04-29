using System;
using System.Collections.Generic;
using System.Diagnostics;

////namespace Merrigan0.Internal.Nodes {
////    [Untested]
////    internal class ConcatenateArrayNode : ArrayNode {
////        private ArrayNode array1;
////        private ArrayNode array2;

////        public override long Length { get { return array1.Length + array2.Length; } }

////        public ConcatenateArrayNode(ArrayNode array1, ArrayNode array2)
////        { 
////            // If the value is not compliant with the distinctness and sort compareResult, keep them
////            /////

////            this.array1 = array1;
////            this.array2 = array2;
////        }

////        /// <summary>
////        /// Copies the elements of the ICollection<T> to an Array, starting at a particular Array index.
////        /// </summary>
////        /// <param name="loadedItems">The one-dimensional Array that is the destination of the elements copied from ICollection<T>. The Array must have zero-based indexing.</param>
////        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
////        /// <exception cref="ArgumentNullException">array is null.</exception>
////        /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
////        public override void CopyTo(Node[] array, long arrayIndex) {
////            array1.CopyTo(array, arrayIndex);
////            array2.CopyTo(array, arrayIndex + array1.Length);
////        }

////        ///// Special concatenated enumerator?

////        protected override T GetCharacter(long i) {
////            if (i < array1.Length) {
////                return array1[i];
////            }
////            return array2[i - array1.Length];
////        }
////    }
////}
