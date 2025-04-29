using System;
using System.Collections.Generic;
using System.Diagnostics;

////namespace Merrigan0.Internal.Nodes {
////    [Untested]
////    internal class InsertItemArrayNode : ArrayNode {
////        private ArrayNode baseArray;
////        private long i;
////        private Node item;

////        public override long Length { get { return baseArray.Length + 1; } }

////        public InsertItemArrayNode(ArrayNode baseArray, long i, Node item)
////        { 
////            // If the value is not compliant with the distinctness and sort compareResult, keep them
////            /////

////            this.baseArray = baseArray;
////            this.i = i;
////            this.item = item;
////        }

////        /// <summary>
////        /// Copies the elements of the ICollection<T> to an Array, starting at a particular Array index.
////        /// </summary>
////        /// <param name="loadedItems">The one-dimensional Array that is the destination of the elements copied from ICollection<T>. The Array must have zero-based indexing.</param>
////        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
////        /// <exception cref="ArgumentNullException">array is null.</exception>
////        /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
////        public override void CopyTo(Node[] array, long arrayIndex) {
////            baseArray.Subarray(0, i).CopyTo(array, arrayIndex);
////            array[arrayIndex + i] = item;
////            baseArray.Subarray(i, baseArray.Length - i).CopyTo(array, arrayIndex + i + 1);
////        }

////        ///// Special concatenated enumerator?

////        protected override Node GetCharacter(long i) {
////            if (i < this.i) {
////                return baseArray[i];
////            }
////            if (i > this.i) {
////                return baseArray[i - 1];
////            }
////            return item;
////        }
////    }
////}
