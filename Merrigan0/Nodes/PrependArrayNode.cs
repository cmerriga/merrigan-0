using System;
using System.Collections.Generic;
using System.Diagnostics;

////namespace Merrigan0.Internal.Nodes {
////    [Untested]
////    internal class PrependArrayNode : ArrayNode {
////        private ArrayNode previous;
////        private Node item;

////        public override long Length { get { return previous.Length + 1; } }

////        public PrependArrayNode(ArrayNode previous, Node item)
////        { 
////            // If the value is not compliant with the distinctness and sort compareResult, keep them
////            /////

////            this.previous = previous;
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
////            array[0] = item;
////            previous.CopyTo(array, arrayIndex + 1);
////        }

////        ///// Special prepended enumerator?

////        protected override Node GetCharacter(long i) {
////            if (i == 0) {
////                return item;
////            }
////            return previous[i + 1];
////        }
////    }
////}
