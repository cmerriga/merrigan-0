using System;
using System.Collections.Generic;
using System.Diagnostics;

////namespace Merrigan0.Internal.Nodes {
////    [Untested]
////    internal class SubarrayNode : ArrayNode {
////        private ArrayNode baseArray;
////        private long i;
////        private long length;

////        public override long Length { get { return length; } }

////        public SubarrayNode(ArrayNode baseArray, long i, long length) {
////            this.baseArray = baseArray;
////            this.i = i;
////            this.length = length;
////        }

////        protected override Node GetCharacter(long i) {
////            if (i >= length) {
////                throw new IndexOutOfRangeException();
////            }
////            return baseArray[this.i + i];
////        }
////    }
////}
