using System;
using System.Collections.Generic;
using System.Diagnostics;

////namespace Merrigan0.Internal.Nodes {
////    [Untested]
////    internal class RemoveArrayNode : ArrayNode {
////        private ArrayNode previous;
////        private long iRemoved;
////        private long lengthRemoved;

////        public override long Length { get { return previous.Length - lengthRemoved; } }

////        public RemoveArrayNode(ArrayNode previous, long iRemoved, long lengthRemoved)
////        { 
////            // If the value is not compliant with the distinctness and sort compareResult, keep them
////            /////

////            this.iRemoved = iRemoved;
////            this.lengthRemoved = lengthRemoved;
////            this.previous = previous;
////        }

////        /// <summary>
////        /// Copies the elements of the ICollection<T> to an Array, starting at a particular Array index.
////        /// </summary>
////        /// <param name="loadedItems">The one-dimensional Array that is the destination of the elements copied from ICollection<T>. The Array must have zero-based indexing.</param>
////        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
////        /// <exception cref="ArgumentNullException">array is null.</exception>
////        /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
////        public override void CopyTo(Node[] array, long arrayIndex) {
////            previous.Subarray(0, iRemoved).CopyTo(array, arrayIndex);
////            previous.Subarray(iRemoved + lengthRemoved, previous.Length - lengthRemoved).CopyTo(array, arrayIndex + iRemoved);
////        }

////        ///// Special removed enumerator?

////        protected override Node GetCharacter(long i) {
////            if (i < iRemoved) {
////                return previous[i];
////            }
////            return previous[i - lengthRemoved];
////        }
////    }
////}
