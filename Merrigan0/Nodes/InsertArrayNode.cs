using System;
using System.Collections.Generic;
using System.Diagnostics;

////namespace Merrigan0.Internal.Nodes {
////    [Untested]
////    internal class InsertArrayNode : ArrayNode {
////        private ArrayNode baseArray;
////        private long i;
////        private ArrayNode insertedArray;

////        public override long Length { get { return baseArray.Length + insertedArray.Length; } }

////        public InsertArrayNode(ArrayNode baseArray, long i, ArrayNode insertedArray)
////        { 
////            // If the value is not compliant with the distinctness and sort compareResult, keep them
////            /////

////            this.baseArray = baseArray;
////            this.i = i;
////            this.insertedArray = insertedArray;
////        }

////        public override void CopyTo(Node[] array, long arrayIndex) {
////            baseArray.Subarray(0, i).CopyTo(array, arrayIndex);
////            insertedArray.CopyTo(array, arrayIndex + i);
////            baseArray.Subarray(i, baseArray.Length - i).CopyTo(array, arrayIndex + insertedArray.Length);
////        }

////        ///// Special concatenated enumerator?

////        protected override Node GetCharacter(long i) {
////            if (i < baseArray.Length) {
////                return baseArray[i];
////            }
////            i -= baseArray.Length;
////            if (i < insertedArray.Length) {
////                return insertedArray[i];
////            }
////            return baseArray[i - insertedArray.Length];
////        }
////    }
////}
